using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.Sync
{
    public class SyncService : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _workDir;
        private readonly StringBuilder _logBuilder;
        private readonly string _baseApiUrl;
        private readonly string _syncedFileDir;
        private readonly string _logFileDir;
        private readonly LogLevel _minLogLevel;
        public DateTime LastSyncDate { get; private set; }

        public SyncService(string workDir, string baseApiUrl, LogLevel logLevel, ILogger logger)
        {
            _minLogLevel = logLevel;
            _workDir = workDir;
            _baseApiUrl = baseApiUrl;
            _logBuilder = new();
            _syncedFileDir = Path.Combine(Directory.GetCurrentDirectory(), _workDir, "Enviados");
            _logFileDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            _logger = logger;
        }

        public async Task SyncAllMeasureData()
        {
            try
            {
                // get all files pending to sync
                var files = GetFiles(_workDir);
                // init rest service
                var syncApiService = RestService.For<SyncApiRefit>(_baseApiUrl,
                                                                        new RefitSettings
                                                                        {
                                                                            ContentSerializer = new SystemTextJsonContentSerializer(GetSerializationOptions())
                                                                        });
                // take the first 10
                foreach (var file in files.Take(10))
                {
                    // get file data deserialized
                    var machineData = GetFileData(file);
                    if (machineData == null)
                    {
                        continue;
                    }

                    WriteLog($"Enviando arquivo: {file.Name}", LogLevel.Trace);
                    WriteLog($"Arquivo: {file.Name} - Content: {SerializeToLog(machineData)}", LogLevel.Debug);

                    //sync to Rest Api
                    var apiResponse = await syncApiService.SyncMachineData(machineData);
                    if (!apiResponse.IsSuccessStatusCode)
                    {
                        WriteLog($"Erro ao sincronizar o arquivo: {file.Name}. Erro: {apiResponse.StatusCode}-{apiResponse.ReasonPhrase}. Response: {await apiResponse.Content.ReadAsStringAsync()}", LogLevel.Error);

                        //wait 1 seg
                        await Task.Delay(1000);

                        //increment retrycount in the file name
                        ChangeRetryCount(file);
                        continue;
                    }
                    WriteLog($"Arquivo sincronizado com sucesso: Status code: {apiResponse.StatusCode} - Headers: {apiResponse.Headers}", LogLevel.Debug);
                    //wait 1 seg
                    await Task.Delay(1000);

                    // move to synced dir
                    MoveSyncedFile(file);
                    LastSyncDate = DateTime.Now;
                }

                // remove old synced files
                RemoveOldData();
            }
            catch (Exception ex)
            {
                WriteLog("Erro ao tentar sincronizar os arquivos com o servidor", ex);
            }
        }

        private MachineData GetFileData(FileInfo file)
        {
            try
            {
                var fileText = File.ReadAllText(file.FullName);

                var machineData = JsonSerializer.Deserialize<MachineData>(fileText, GetSerializationOptions());

                return machineData;
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao deserializar os dados do arquivo json: {file.FullName}", ex);
                return null;
            }
        }

        private IEnumerable<FileInfo> GetFiles(string appSubDir, string fileType = "*.json")
        {
            var fileInfoList = new List<FileInfo>();
            try
            {
                var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), appSubDir), fileType);

                foreach (var item in files)
                {
                    fileInfoList.Add(new FileInfo(item));
                }
            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao recuperar os arquivos do diretório: {Path.Combine(Directory.GetCurrentDirectory(), appSubDir)}", ex);
            }

            return fileInfoList;
        }

        private void ChangeRetryCount(FileInfo file)
        {
            try
            {
                string fileName = file.Name;
                int retryCount = 1;

                //check retry count
                if (char.IsLetter(fileName.First()) && fileName.Take(4).Contains('_'))
                {
                    //filtra apenas numeros e pega os dois primeiros
                    retryCount = int.Parse(new string(fileName.Take(3).Where(c=> char.IsDigit(c)).ToArray()));

                    //increment retry count
                    retryCount = retryCount + 1;

                    //circuit-break
                    //if hits 30 retrys, move file to "sync" folder
                    if (retryCount > 30)
                    {
                        MoveSyncedFile(file);
                        return;
                    }

                    //change file name
                    fileName = $"R{retryCount}_{GetOriginalFileName(fileName)}";
                }
                else
                {
                    //change file name first time
                    fileName = $"R{retryCount}_{fileName}";
                }
                

                // move file to synced dir
                File.Move(file.FullName, Path.Combine(file.DirectoryName, fileName));
            }
            catch (Exception ex)
            {
                WriteLog("Erro ao tentar aumentar o retry-count do arquivo que esta sendo sincronizado!", ex);
            }
        }

        private void MoveSyncedFile(FileInfo file)
        {
            try
            {
                // if synced dir not exists, create it
                if (!Directory.Exists(_syncedFileDir))
                {
                    Directory.CreateDirectory(_syncedFileDir);
                }

                // move file to synced dir
                File.Move(file.FullName, Path.Combine(_syncedFileDir, file.Name));
            }
            catch (Exception ex)
            {
                WriteLog($"Erro mover o arquivo já enviado. Dir origem:{file.FullName} | Para: {Path.Combine(_syncedFileDir, file.Name)}", ex);

                // try to rename the source file
                // the cause of a failed move generally is the file with the same name already existing in the target dir
                try
                {
                    file.MoveTo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), _workDir), "e1_" + file.Name));
                }
                catch (Exception)
                {
                    //silence
                }
            }
        }

        /// <summary>
        /// Remove arquivos enviados e de log antigos
        /// </summary>
        private void RemoveOldData()
        {
            try
            {
                //remove arquivos ENVIADOS antigos
                var syncFiles = GetFiles(_syncedFileDir, "*");

                foreach (var item in syncFiles)
                {
                    if ((DateTime.Now - item.CreationTime).TotalDays > 3)
                    {
                        File.Delete(item.FullName);
                    }    
                }

                //remove arquivos de LOG antigos
                var logFiles = GetFiles(_logFileDir, "*");

                foreach (var item in logFiles)
                {
                    if ((DateTime.Now - item.CreationTime).TotalDays > 30)
                    {
                        File.Delete(item.FullName);
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog($"Erro ao tentar limpar os arquivos antigos do diretório de enviados", ex);
            }
        }

        private string GetOriginalFileName(string currentFileName)
        {
            if (currentFileName.Contains("_"))
            {
                string retorno = string.Empty;
                var fileNameParts = currentFileName.Split('_', StringSplitOptions.RemoveEmptyEntries);
                if (fileNameParts.First().Contains("R"))
                {
                    foreach (var part in fileNameParts.Skip(1))
                    {
                        retorno += part + "_";
                    }

                    retorno = retorno.Substring(0, retorno.Length - 1);

                    return retorno;
                }
            }

            return currentFileName;
        }

        public void Dispose()
        {
            _logBuilder.Clear();
        }

        protected void WriteLog(string message, LogLevel logLevel = LogLevel.Information)
        {
            if (logLevel >= _minLogLevel)
            {
                _logBuilder.AppendLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {message}");
            }

            _logger.Log(logLevel, message);

            ClearLogBuilder();
        }

        protected void WriteLog(string message, Exception ex, LogLevel logLevel = LogLevel.Error)
        {
            if (ex == null)
            {
                WriteLog(message, logLevel);
                return;
            }

            if (logLevel >= _minLogLevel)
            {
                _logBuilder.AppendLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - ERROR: {message} - EX: {ex}");
            }

            ClearLogBuilder();

            _logger.Log(logLevel, ex, message);
        }

        private void ClearLogBuilder(bool force = false)
        {
            if (_logBuilder != null)
            {
                if (force || _logBuilder.Length > 30000 ) 
                {
                    _logBuilder.Clear();
                }
            }
        }

        private string SerializeToLog(MachineData machineData)
        {
            return JsonSerializer.Serialize(machineData, GetSerializationOptions());
        }

        public string GetCurrentLog()
        {
            if (_logBuilder == null)
                return string.Empty;

            var logReturn = _logBuilder.ToString();
            ClearLogBuilder();

            return logReturn;
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
