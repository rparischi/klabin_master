using Klabin.Rml.ClientLogic.SocketHandlers;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.MachineReaders
{
    public class TCP_MachineReader : ReaderBase
    {
        private const string SeparatorValue = ":";
        private const string CapturedDataLengthName = "Length";
        private const string CapturedDataDiameterName = "Diameter";
        private const string HeaderTelegram = "@0010!MA";
        private readonly TcpReaderConfig _config;
        private AsynchronousSocketPassiveHandler socketListener;
        private Task listnerTask;
        private MachineData lastReadMachineData;
        private object syncObject;


        public TCP_MachineReader(TcpReaderConfig config, LogLevel minLogLevel, ILogger logger, CancellationToken cancellationToken) : base(minLogLevel, logger, cancellationToken)
        {
            try
            {
                _config = config;

                Open();
                successfullyInitialized = true;
                syncObject = new object();
            }
            catch (Exception)
            {
                successfullyInitialized = false;
            }
        }

        ~TCP_MachineReader()
        {
            Dispose();
        }

        public override Task<MachineData> GetMachineData()
        {
            if (lastReadMachineData != null)
            {
                // block concurrent read/write operations
                lock (syncObject)
                {
                    // notify observer of the result
                    NotifyObservers(lastReadMachineData);

                    // clear last result state
                    lastReadMachineData = null;
                }
            }
            
            return Task.FromResult(lastReadMachineData);
        }

        private void ReceiveData(string rawData)
        {
            if (string.IsNullOrWhiteSpace(rawData))
                return;

            try
            {
                // init new transfer object
                var machineData = new MachineData(_config.MachineNumber, _config.MachineType, _config.CapturedDataConfigList)
                {
                    RmlRawData = rawData,
                    ReadTime = DateTime.Now,
                    DriverType = _config.ReaderType.ToString()
                };

                // try to process the machine data
                TryGetMessure(machineData);

                // save all the operation log
                machineData.OperationLog = GetReaderLog();

                // block concurrent read/write operations
                lock (syncObject)
                {
                    lastReadMachineData = machineData;
                }
            }
            catch (Exception ex)
            {
                WriteLog("Erro ao processar os dados recebidos do socket", ex);
            }
        }

        private void TryGetMessure(MachineData machineData)
        {
            var fieldDataLength = machineData.CapturedDataList.FirstOrDefault(x => x.Name == CapturedDataLengthName);
            if (fieldDataLength == null)
            {
                return;
            }
            var fieldDataDiameter = machineData.CapturedDataList.FirstOrDefault(x => x.Name == CapturedDataDiameterName);
            if (fieldDataDiameter == null)
            {
                return;
            }


            if (string.IsNullOrWhiteSpace(machineData.RmlRawData))
            {
                return;
            }

            //check if the message contains the main header with the Data:
            //@0010!MA
            //The data should be:
            //@0010!MA:Rxxxx:Sxxxx;
            //Where Rxxx is the diameter and Sxxxx is the Length
            if (machineData.RmlRawData.Contains(HeaderTelegram))
            {
                var telegramValues = machineData.RmlRawData.Split(SeparatorValue, StringSplitOptions.RemoveEmptyEntries);

                //check data consistency
                if (telegramValues.Length < 3)
                {
                    WriteLog($"Os valores recebidos não estão de acordo com o padrão esperado! Valores recebidos: {machineData.RmlRawData}. Espera-se 3 grupos de informação separados por ':'. Ex: @0010!MA:Rxxxx:Sxxxx;", LogLevel.Warning);
                    return;
                }

                //get the second group - index 1
                var diameter = new string(telegramValues[1].Replace("R", string.Empty).Where(c => char.IsLetterOrDigit(c)).ToArray());

                //get the third group - index 2
                var length = new string(telegramValues[2].Replace("S", string.Empty).Where(c => char.IsLetterOrDigit(c)).ToArray());

                WriteLog($"Valores lidos - Metragem: {length} | Diametro: {diameter}", LogLevel.Information);

                fieldDataDiameter.TrySetValue(diameter);
                fieldDataLength.TrySetValue(length);
            }
        }


        #region Open / Close Procedures
        private bool Open()
        {
            try
            {
                //start a new async socket listner
                socketListener = new AsynchronousSocketPassiveHandler(ReceiveData, WriteLog, _cancellationToken);

                // start a new Task (thread) and pool de TCP IP/Port
                listnerTask = Task.Run(() => socketListener.StartListening(_config));

                return true;
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao abir a conexão TCP com IP: {_config.Address} na Porta: {_config.Port}. Erro: {ex.Message}", ex);
                return false;
            }
        }

        private bool Close()
        {
            try
            {
                if (listnerTask == null || socketListener == null)
                    return false;

                socketListener.Stop();
                socketListener.Dispose();
                socketListener = null;

                return true;
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao fechar a conexão TCP com IP: {_config.Address} na Porta: {_config.Port}. Erro: {ex.Message}", ex);
                return false;
            }
        }

        public override void Dispose()
        {
            if (socketListener != null)
            {
                Close();
            }
            socketListener = null;
        }
        #endregion
    }    
}
