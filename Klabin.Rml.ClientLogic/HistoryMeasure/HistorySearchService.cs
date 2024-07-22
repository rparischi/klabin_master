using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Klabin.Rml.ClientLogic.HistoryMeasure
{
    public class HistorySearchService
    {
        private readonly ILogger _logger;
        private readonly string _workDir;
        private readonly string _baseApiUrl;
        private readonly int _numberOfDays;
        private readonly string _syncedFileDir;
        private readonly LogLevel _minLogLevel;
        private readonly string _baseApiSrpUrl;

        public HistorySearchService(string workDir, string baseApiUrl, string baseApiSrpUrl, int numberOfDays, LogLevel logLevel, ILogger logger)
        {
            _minLogLevel = logLevel;
            _workDir = workDir;
            _baseApiUrl = baseApiUrl;
            _baseApiSrpUrl = baseApiSrpUrl;
            _numberOfDays = numberOfDays;
            _syncedFileDir = Path.Combine(Directory.GetCurrentDirectory(), _workDir, "Enviados");
            _logger = logger;
        }

        public async Task<List<HistoryMachineDataResponseDto>> SearchHistoryAsync(HistoryMachineDataRequest request)
        {
            List<HistoryMachineDataResponseDto> listResponse = new();

            if (request.Synchronized == null)
            {
                listResponse.AddRange(await GetHistoryFromApiAsync(request));
                listResponse.AddRange(GetHistoryFromLocal(request));
            }
            else if (request.Synchronized.Value)
            {
                listResponse.AddRange(await GetHistoryFromApiAsync(request));
            }
            else if (request.Synchronized.Value == false)
            {
                listResponse.AddRange(GetHistoryFromLocal(request));
            }

            listResponse = listResponse.OrderBy(x => x.MachineNumber)
                                       .ThenByDescending(x => x.RollDate)
                                       .ThenByDescending(x => x.ReadTime)
                                       .ThenByDescending(x => string.IsNullOrWhiteSpace(x.RollNumber) ? 0 : int.Parse(x.RollNumber))
                                       .ThenByDescending(x => string.IsNullOrWhiteSpace(x.CutNumber) ? 0 : int.Parse(x.CutNumber))
                                       .ToList();

            return listResponse;
        }

        public async Task<(HistoryMachineDataResponseDto model, bool success)> SearchLastMachineDataAsync(LastMachineDataRequest request)
        {
            return await GetLastMachineDataFromApiAsync(request);
        }

        private async Task<(HistoryMachineDataResponseDto model, bool success)> GetLastMachineDataFromApiAsync(LastMachineDataRequest request)
        {
            try
            {
                var restService = RestService.For<IHistoryRefit>(_baseApiUrl);

                var result = await restService.GetLastMachineDataResponse(request);

                if (result?.Metadata?.Warning?.Any() == true)
                {
                    _logger.LogError($"Erro ao buscar a última leitura online de leituras. Mensagem: {result.Metadata.Warning.Select(x => x.Code).Aggregate((x, y) => x + "; " + y)}");
                    return (null, false);
                }

                return (result.Data, true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar a última leitura online de leituras. Mensagem: {ex.Message}");
                return (null, false);
            }
        }

        private async Task<List<HistoryMachineDataResponseDto>> GetHistoryFromApiAsync(HistoryMachineDataRequest request)
        {
            try
            {
                var restService = RestService.For<IHistoryRefit>(_baseApiUrl);

                var result = await restService.GetMachineHistoryMachineMeasure(request);

                if (result?.Metadata?.Warning?.Any() == true)
                {
                    _logger.LogError($"Erro ao buscar o histórico online de leituras. Mensagem: {result.Metadata.Warning.Select(x => x.Code).Aggregate((x, y) => x + "; " + y)}");
                    return new List<HistoryMachineDataResponseDto>();
                }

                var history = result.Data;

                //extract projecting each captured data to an object in the response list
                var resultExtracted = new List<HistoryMachineDataResponseDto>(history.Count);
                foreach (var item in history)
                {
                    resultExtracted.AddRange(item.ExtractMeasureValues());
                }

                //mark all the response itens as synchronized
                resultExtracted.ForEach(r => r.Synchronized = true);

                return resultExtracted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o histórico online de leituras. Mensagem: {ex.Message}");
                return new List<HistoryMachineDataResponseDto>();
            }
        }

        public async Task<List<HistoryLinearMeasureDataResponseDto>> GetLinearMeasureHistory(HistoryLinearMeasureDataRequest request)
        {
            try
            {
                request.FinalRollDate = request.RollDate.AddDays(-_numberOfDays);

                var restService = RestService.For<IHistorySafariRefit>(_baseApiSrpUrl);

                return await restService.GetLinearMeasureHistory(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o histórico online de leituras. Mensagem: {ex.Message}");
                return new List<HistoryLinearMeasureDataResponseDto>();
            }
        }

        private List<HistoryMachineDataResponseDto> GetHistoryFromLocal(HistoryMachineDataRequest request)
        {
            List<HistoryMachineDataResponseDto> listResponse = new();

            try
            {
                // get all files pending to sync
                var files = GetFiles(_workDir);

                // read all file data and load into the response list
                foreach (var file in files)
                {
                    var machineData = GetFileData(file);
                    listResponse.AddRange(MapToResponseType(machineData));
                }

                // filter the result with request data
                listResponse = FilterResult(listResponse, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o histórico local de leituras. Mensagem: {ex.Message}");
            }

            return listResponse;
        }

        private MachineData GetFileData(FileInfo file)
        {
            try
            {
                var fileText = File.ReadAllText(file.FullName);

                var machineData = JsonSerializer.Deserialize<MachineData>(fileText, options: GetConfigSerializationOptions());

                return machineData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao recuperar o arquivo do histórico local de leituras, Arquivo: {file.FullName}. Mensagem: {ex.Message}");
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
                _logger.LogError(ex, $"Erro ao recuperar os arquivos do diretório: {Path.Combine(Directory.GetCurrentDirectory(), appSubDir)}");
            }

            return fileInfoList;
        }

        private List<HistoryMachineDataResponseDto> MapToResponseType(MachineData machineData)
        {
            List<HistoryMachineDataResponseDto> listResponse = new();

            foreach (var item in machineData.CapturedDataList.OrderBy(c => c.Position))
            {
                HistoryMachineDataResponseDto response = new();

                response.MachineNumber = machineData.MachineNumber;
                response.RollNumber = machineData.RollNumber;
                response.CutNumber = machineData.CutNumber;
                response.RmlRawData = machineData.RmlRawData;
                response.ReadTime = machineData.ReadTime;
                response.RollDate = machineData.RollDate;
                response.Synchronized = false;

                response.MeasureType = item.DescriptionName;
                response.MeasureValue = item.Value?.ToString();

                listResponse.Add(response);
            }

            return listResponse;
        }

        private List<HistoryMachineDataResponseDto> FilterResult(List<HistoryMachineDataResponseDto> listResponse, HistoryMachineDataRequest request)
        {
            var result = listResponse.Where(r => r.MachineNumber == request.MachineNumber);

            if (!string.IsNullOrWhiteSpace(request.MeasureType))
            {
                result = result.Where(r => r.MeasureType == request.MeasureType);
            }
            if (!string.IsNullOrWhiteSpace(request.RollNumber))
            {
                result = result.Where(r => r.RollNumber == request.RollNumber);
            }
            if (!string.IsNullOrWhiteSpace(request.CutNumber))
            {
                result = result.Where(r => r.CutNumber == request.CutNumber);
            }
            if (!string.IsNullOrWhiteSpace(request.DriverType))
            {
                result = result.Where(r => r.DriverType == request.DriverType);
            }
            if (request.InitialDate.HasValue)
            {
                result = result.Where(r => r.ReadTime.Date >= request.InitialDate.Value.Date);
            }
            if (request.FinalDate.HasValue)
            {
                result = result.Where(r => r.ReadTime.Date <= request.FinalDate.Value.Date);
            }

            return result.ToList();
        }

        public static JsonSerializerOptions GetConfigSerializationOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
