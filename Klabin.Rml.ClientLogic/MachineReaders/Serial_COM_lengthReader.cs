using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.MachineReaders
{
    /// <summary>
    /// Machine 7 - Rebobinadeira (rewinder) reader
    /// The code supress bellow is intentent to supress iOS/Android compatibility warning
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Only windows plataform to be targeted")]
    public class Serial_COM_lengthReader : ReaderBase
    {
        private SerialPort _serialCOM;
        private const string CapturedDataLengthName = "Length";
        private const string CapturedDataVelocityName = "Velocity";
        private const string CapturedDataDiameterName = "Diameter";
        private readonly SerialReaderConfig _config;
        private int lastLengthData;
        private int lastVelocityData;


        public Serial_COM_lengthReader(SerialReaderConfig config, LogLevel minLogLevel, ILogger logger, CancellationToken cancellationToken) : base(minLogLevel, logger, cancellationToken)
        {
            try
            {
                _config = config;

                Open(_config.PortName, _config.BaudRate, _config.DataBits, _config.Parity, _config.StopBits);
                successfullyInitialized = true;
            }
            catch (Exception)
            {
                successfullyInitialized = false;
            }
        }

        ~Serial_COM_lengthReader()
        {
            Dispose();
        }

        public override Task<MachineData> GetMachineData()
        {
            var rmlData = new MachineData(_config.MachineNumber, _config.MachineType, _config.CapturedDataConfigList)
            {
                DriverType = _config.ReaderType.ToString()
            };

            try
            {
                string velocityRawData = ReadVelocity(rmlData);
                int length = 0;
                string lengthRawData;

                if (!string.IsNullOrWhiteSpace(velocityRawData))
                {
                    //parse velocity to int
                    var velocity = ParseRawToInt(velocityRawData);


                    //set the field value
                    var fieldVelocity = SetField(rmlData, CapturedDataVelocityName, velocity.ToString());

                    //if velocity is incresing and last length has value
                    //then the winder is speedig up, so we must "clear" last length
                    if (fieldVelocity.CompareFieldValueGreaterThan(50) && 
                        lastLengthData > 0 && 
                        velocity > lastVelocityData)
                    {
                        lastLengthData = 0;
                    }

                    //if velocity is less than a min value, the winder is stoping...
                    if (fieldVelocity.CompareFieldValueLessThan(10))
                    {
                        //get length from COM
                        lengthRawData = ReadLength(rmlData);

                        //parse raw length
                        length = ParseRawToInt(lengthRawData);

                        //if length is significant and changed from last length, than capture it
                        if (length > 100 && length != lastLengthData)
                        {
                            SetField(rmlData, CapturedDataLengthName, length.ToString());
                            lastLengthData = length;
                        }
                    }

                    WriteLog($"Length: {length} | LastLength: {lastLengthData} | Velocity: {velocity} | LastVelocity: {lastVelocityData}", LogLevel.Trace);

                    //store the last velocity
                    lastVelocityData = velocity;
                }

                rmlData.ReadTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                WriteLog("Erro grave ao obter os dados", ex);
            }

            rmlData.OperationLog = GetReaderLog();

            NotifyObservers(rmlData);

            return Task.FromResult(rmlData);
        }

        private string ReadVelocity(MachineData rmlData)
        {
            //writes V - in the COM
            //this will invoke the another side to write the Velocity value in the COM
            //the value must be sent with "\n\r" terminator in HEX
            _serialCOM.Write("V\x00\r");

            string readValue = string.Empty;

            //try for 5 times to read 
            for (int i = 0; i < 5; i++)
            {
                //read all existing value in COM
                readValue = _serialCOM.ReadExisting();

                //check if we received the value
                if (!string.IsNullOrWhiteSpace(readValue))
                {
                    break;
                }

                //wait until another Lookup
                Thread.Sleep(_config.LookpUpWaitTime);
            }

            //saves raw data
            rmlData.RmlRawData = readValue;

            WriteLog($"{_config.PortName} - Enviado: V | Retornado:{readValue}", LogLevel.Trace);

            return readValue;
        }

        private string ReadLength(MachineData rmlData)
        {
            //writes L - in the COM
            //this will invoke the another side to write the Length value in the COM
            //the value must be sent with "\n\r" terminator in HEX
            _serialCOM.Write("L\x00\r");

            string readValue = string.Empty;

            //try for 5 times to read 
            for (int i = 0; i < 5; i++)
            {
                //read all existing value in COM
                readValue = _serialCOM.ReadExisting();

                //check if we received the value
                if (!string.IsNullOrWhiteSpace(readValue))
                {
                    break;
                }

                //wait until another Lookup
                Thread.Sleep(_config.LookpUpWaitTime);
            }

            //saves raw data
            rmlData.RmlRawData = readValue;

            WriteLog($"{_config.PortName} - Enviado: L | Retornado:{readValue}", LogLevel.Trace);

            return readValue;
        }

        private MachineCapturedData SetField(MachineData machineData, string fieldName, string value)
        {
            //finds length postion in the config 
            var field = machineData.CapturedDataList.FirstOrDefault(x => x.Name == fieldName);
            if (field == null || field.IsEmpty())
            {
                //abort the process with an error
                WriteLog($"Could not find the field: {CapturedDataVelocityName} in the configured field list", LogLevel.Error);
                return field;
            }

            //set the field value
            if (!field.TrySetValue(value))
            {
                WriteLog($"Could not set the field: {field.Name} value with data: {value}", LogLevel.Warning);
            }

            return field;
        }

        private int ParseRawToInt(string rawValue)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(rawValue))
                return result;
            
            if (rawValue.Contains("."))
            {
                var intPart = rawValue.Substring(0, rawValue.IndexOf('.'));

                int.TryParse(intPart, out result);
                return result;
            }

            int.TryParse(rawValue, out result);

            return result;
        }

        #region Open / Close Procedures
        private bool Open(string portName, int baudRate, int databits, Parity parity, StopBits stopBits)
        {
            _serialCOM = new SerialPort();

            //Ensure port isn't already opened:
            if (!_serialCOM.IsOpen)
            {
                //Assign desired settings to the serial port:
                _serialCOM.PortName = portName;
                _serialCOM.BaudRate = baudRate;
                _serialCOM.DataBits = databits;
                _serialCOM.Parity = parity;
                _serialCOM.StopBits = stopBits;


                //These timeouts are default and cannot be editted through the class at this point:
                _serialCOM.ReadTimeout = 300;
                _serialCOM.WriteTimeout = 500;

                try
                {
                    _serialCOM.Open();
                }
                catch (Exception ex)
                {
                    WriteLog("Error opening " + portName + ": " + ex.Message, ex);
                    return false;
                }
                WriteLog(portName + " opened successfully");
                return true;
            }
            else
            {
                WriteLog(portName + " already opened");
                return false;
            }
        }

        
        private bool Close()
        {
            if (_serialCOM.IsOpen)
            {
                try
                {
                    _serialCOM.Close();
                }
                catch (Exception ex)
                {
                    WriteLog("Error closing " + _serialCOM.PortName + ": " + ex.Message, ex);
                    return false;
                }
                WriteLog(" closed successfully");
                return true;
            }
            else
            {
                WriteLog(_serialCOM.PortName + " is not open");
                return true;
            }
        }

        public override void Dispose()
        {
            if (_serialCOM != null)
            {
                Close();
                _serialCOM.Dispose();
            }

            _serialCOM = null;
        }
        #endregion


    }
}
