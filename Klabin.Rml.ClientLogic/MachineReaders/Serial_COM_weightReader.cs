using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.MachineReaders
{
  /// <summary>
  /// Machine 7 - Rebobinadeira (rewinder) reader
  /// The code supress bellow is intentent to supress iOS/Android compatibility warning
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Only windows plataform to be targeted")]
  public class Serial_COM_weightReader : ReaderBase
  {
    private SerialPort _serialCOM;
    private const string CapturedDataWeightName = "Weight";
    private readonly SerialReaderConfig _config;
    private int lastWeightData;


    public Serial_COM_weightReader(SerialReaderConfig config, LogLevel minLogLevel, ILogger logger, CancellationToken cancellationToken) : base(minLogLevel, logger, cancellationToken)
    {
      try
      {
        _config = config;

        Open(_config.PortName, _config.BaudRate, _config.DataBits, _config.Parity, _config.StopBits);
        successfullyInitialized = true;
      }
      catch (Exception ex)
      {
        WriteLog("Erro ao fechar conexão com a porta serial", ex);
        successfullyInitialized = false;
      }
    }

    ~Serial_COM_weightReader()
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

        string weightRawData = ReadWeight(rmlData);

        if (!string.IsNullOrWhiteSpace(weightRawData))
        {
          //parse weight to int
          var weight = ParseRawToInt(weightRawData);

          //set the field value

          var fieldWeight = SetField(rmlData, CapturedDataWeightName, weight.ToString());

          // weight
          lastWeightData = weight;

          WriteLog($" Weight: {weight} | LastWeight: {lastWeightData}", LogLevel.Information);
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

    private string ReadWeight(MachineData rmlData)
    {
      int validWeigthReadNeeded = 5;
      int validWeigthReaded = 0;
      string rawData = string.Empty;
      string currentRawData = string.Empty;
      string firstRawData = string.Empty;
      string readValue = string.Empty;

      //read all existing value in COM
      readValue = _serialCOM.ReadExisting();

      firstRawData = _config.MachineType == MachineType.ReelWeightToledo ? ConvertToIntToledo(readValue) : readValue;
      if (ParseRawToInt(firstRawData) == 0)
      {
        WriteLog($"O Valor deve ser maior que: {ParseRawToInt(firstRawData)} , para ser salvo. ", LogLevel.Information);
        return string.Empty;
      }

      //Sleeps to wait for REALLY stable weight
      Thread.Sleep(1000);

      for (int i = 0; i < validWeigthReadNeeded; i++)
      {
        rawData = _serialCOM.ReadExisting();

        currentRawData = _config.MachineType == MachineType.ReelWeightToledo ? ConvertToIntToledo(readValue) : readValue;

        WriteLog($"Leitura {i}: {ParseRawToInt(currentRawData)}", LogLevel.Information);
        WriteLog($"ValorOriginal: {ParseRawToInt(firstRawData)}  |  ValorLido: {ParseRawToInt(currentRawData)}", LogLevel.Information);

        if (ParseRawToInt(firstRawData) == ParseRawToInt(currentRawData))
        {
          validWeigthReaded++;
        }
        else
        {
          //long unstable weigth
          WriteLog($"Leitura invalida. Leitura atual: {ParseRawToInt(rawData)} - Buscando peso instável....", LogLevel.Information);

          var unstableData = _serialCOM.ReadExisting();

          WriteLog($"Peso (instável): {ParseRawToInt(unstableData)} ", LogLevel.Information);

          //if unstable, stop the process and try again on next time
          break;
        }

        //Sleeps to wait for REALLY stable weight
        Thread.Sleep(1000);
      }

      //check for consecutives valid read AND for a valid meassure
      if (validWeigthReaded != validWeigthReadNeeded)
      {
        return string.Empty;
      }

      //saves raw data
      rmlData.RmlRawData = _config.MachineType == MachineType.ReelWeightToledo ? currentRawData : readValue;

      WriteLog($"{_config.PortName} - Enviado: V | Retornado:{rmlData.RmlRawData}", LogLevel.Information);

      if (_config.MachineType == MachineType.ReelWeightToledo)
      {
        return currentRawData;
      }
      else
      {
        return readValue;
      }
     
    }

    private MachineCapturedData SetField(MachineData machineData, string fieldName, string value)
    {

      var field = machineData.CapturedDataList.FirstOrDefault(x => x.Name == fieldName);
      var teste = machineData.CapturedDataList.FirstOrDefault(x => x.Name != fieldName);

      if (field == null || field.IsEmpty())
      {
        //abort the process with an error
        WriteLog($"Could not find the field: {CapturedDataWeightName} in the configured field list", LogLevel.Error);
        return field;
      }

      //set the field value
      if (!field.TrySetValue(value))
      {
        WriteLog($" ERRO: Could not set the field: {field.Name} value with data: {value}", LogLevel.Warning);
      }

      return field;
    }


    public  string ConvertToIntToledo(string readValue)
    {

      var regex1 = new Regex(@"\\0\s*0*(\d{11})\d*");
      var regex2 = new Regex(@"\u0002i0  \d*");
      var regex3 = new Regex(@"\u0002\)\s*(\d+\s*\d+\s*\d+\s*\d+)");
      
      Match match = regex1.Match(readValue);

      if (match.Success)
      {
        string extractedWeight = match.Groups[1].Value;

        long weightInGrams = long.Parse(extractedWeight);
        double weightInTons = weightInGrams / 1000000.0;

        WriteLog($"Peso: {weightInTons.ToString("0") + "kg" }");

        return weightInTons.ToString("0") + "kg";
      }

      match = regex2.Match(readValue);
      //Validando segunda opção de regex
      if (match.Success)
      {
        string extractedWeight = match.Groups[0].Value.Replace(" ", "").Replace("i", "").Replace("\u0002", "");

        long weightInGrams; 
        var isLong = long.TryParse(extractedWeight, out weightInGrams);
        if (isLong)
        {
            double weightInTons = weightInGrams / 1000000.0;

            WriteLog($"Peso: {weightInTons.ToString("0") + "kg"}");

            return weightInTons.ToString("0") + "kg";
        }
      }

      match = regex3.Match(readValue);
      //Validando terceira opção de regex
      if (match.Success)
      {
        string extractedWeight = match.Groups[1].Value.Replace(" ", "");

        
        long weightInGrams; 
        var isLong = long.TryParse(extractedWeight, out weightInGrams);
        if (isLong)
        {
            double weightInTons = weightInGrams / 100.0;

            WriteLog($"Peso: {weightInTons.ToString("0") + "kg"}");

            return weightInTons.ToString("0") + "kg";
        }

                /*
        long weightInTons = long.Parse(extractedWeight);
        //double weightInTons = weightInGrams / 1000000.0;

        WriteLog($"Peso: {weightInTons.ToString("0") + "kg" }");

        return weightInTons.ToString("0") + "kg";
                */
      }



      return string.Empty;
    }


    private int ParseRawToInt(string rawValue)
    {
      int result = 0;
      bool checkResult = false;

      WriteLog($"Valor lido: {rawValue}");

      if (string.IsNullOrWhiteSpace(rawValue))
        return result;

      //var regex = _config.MachineType == MachineType.ReelWeightToledo ?  new Regex("\\d{5}kg") : new Regex("\\d{7}kg");
      var output = Regex.Replace(rawValue, @"[^\d]", "");
      if (rawValue.Split("\n").Length > 1)
      {
          output = Regex.Replace(rawValue.Split("\n")[0], @"[^\d]", "");
      }
      /*if (!output.Success)
      {
         return result;
      }

      if (_config.MachineType != MachineType.ReelWeightToledo)
      {
        if (!output.Groups.ContainsKey("0"))
          return result;
      }
      else
      {
        if (output.Success)
        {
          checkResult = int.TryParse(output.Groups[0].Value.Replace("kg", ""), out result);
        }
      }*/
      checkResult = int.TryParse(output, out result);
      return checkResult ? result : 0;
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
