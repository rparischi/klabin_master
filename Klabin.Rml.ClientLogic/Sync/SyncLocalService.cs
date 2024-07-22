using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;

namespace Klabin.Rml.ClientLogic.Sync
{
    public class SyncLocalService
    {
        public const string MEASURE_READ_FILE_NAME = "{0}_measure_{1}.json";
        private readonly string _baseDirectory;
        private readonly ILogger _logger;

        public SyncLocalService(string baseDirectory, ILogger logger)
        {
            _baseDirectory = baseDirectory;
            _logger = logger;
        }        

        public (bool success, string errorMessage) SaveMachineMeasure(MachineData machineData)
        {
            try
            {
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }

                var machineDataJson = JsonSerializer.Serialize(machineData, GetSerializationOptions());

                string fileName = string.Format(MEASURE_READ_FILE_NAME, machineData.MachineNumber, DateTime.Now.ToString("yyMMdd-HHmmss"));

                File.WriteAllText(Path.Combine(_baseDirectory, fileName), machineDataJson);
                return (true, null);
            }
            catch (Exception ex)
            {
                var message = $"Erro ao salvar os dados da leitura.Erro: {ex.Message}";

                WriteLog(message, ex);
                return (false, message);
            }
        }

        protected void WriteLog(string message, Exception ex, LogLevel logLevel = LogLevel.Error)
        {
            if (ex == null)
            {
                WriteLog(message, logLevel);
                return;
            }

            _logger.Log(logLevel, ex, message);
        }

        protected void WriteLog(string message, LogLevel logLevel = LogLevel.Information)
        {
            _logger.Log(logLevel, message);
        }

        public static JsonSerializerOptions GetSerializationOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
