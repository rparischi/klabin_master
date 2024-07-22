﻿using Klabin.Rml.ClientLogic.SocketHandlers;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.MachineReaders
{
    public class TCP_MachineActiveReader : ReaderBase
    {
        private readonly TcpReaderConfig _config;

        private const string CapturedDataWeigthName = "Weight";

        public const string RequestMessage = "S\r\n";

        public const string RetryRequestMessage = "SI\r\n";

        public TCP_MachineActiveReader(TcpReaderConfig config, LogLevel minLogLevel, ILogger logger, CancellationToken token) : base(minLogLevel, logger, token)
        {
            _config = config;

            successfullyInitialized = true;
        }

        ~TCP_MachineActiveReader()
        {
            Dispose();
        }

        public override async Task<MachineData> GetMachineData()
        {
            var machineData = new MachineData();
            try
            {
                var rawData = await GetMachineData(new AsynchronousSocketActiveHandler(WriteLog, _config, _cancellationToken));
                if (string.IsNullOrWhiteSpace(rawData))
                {
                    return machineData;
                }

                if (rawData == "0")
                {
                    return machineData = new MachineData(_config.MachineNumber, _config.MachineType, _config.CapturedDataConfigList)
                    {
                        RmlRawData = rawData
                    };
                }

                machineData = new MachineData(_config.MachineNumber, _config.MachineType, _config.CapturedDataConfigList)
                {
                    RmlRawData = rawData,
                    ReadTime = DateTime.Now,
                    DriverType = _config.ReaderType.ToString()
                };

                // try to process the machine data
                if (!TryGetMessure(machineData))
                {
                    return machineData;
                }

                
                return machineData;
            }
            catch (Exception ex)
            {
                WriteLog("Erro ao buscar os dados do socket", ex);
                return null;
            }
            finally
            {
                // save all the operation log
                machineData.OperationLog = GetReaderLog();

                NotifyObservers(machineData);
            }
        }

        /// <summary>
        /// O objetivo deste método é buscar dados do peso das bobinas no módulo de peso
        /// para isso ele deve receber pelo menos 4 leituras de peso estável idênticas em 4 segundos
        /// do contrário ele discartará o valor e irá considerar isso como "peso instável"
        /// </summary>
        /// <param name="socketActiveHandler"></param>
        /// <returns></returns>
        private async Task<string> GetMachineData(AsynchronousSocketActiveHandler socketActiveHandler)
        {
            int validWeigthReadNeeded = 4;
            int validWeigthReaded = 0;
            string rawData = string.Empty;
            string firstRawData = string.Empty;

            var rawDataSIValidation = await socketActiveHandler.ExecuteRequestAsync(RetryRequestMessage);

            //get first stable weigth
            rawData = await socketActiveHandler.ExecuteRequestAsync(RequestMessage);
            firstRawData = rawData;

            var messureSIValue = !string.IsNullOrWhiteSpace(rawDataSIValidation) ? GetMessureValue(rawDataSIValidation) : "";
            var messureValue = !string.IsNullOrWhiteSpace(rawData) ? GetMessureValue(rawData) : "";

            if (!string.IsNullOrWhiteSpace(messureValue))
            {
                if (int.TryParse(messureValue, out int intResult))
                {
                    if (intResult <= 500)
                    {
                        WriteLog($"Variavel foi limpa! Peso (estável) registrado: {intResult} ", LogLevel.Information);
                        return "0";
                    }
                }
                else if (decimal.TryParse(messureValue, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentUICulture, out decimal decimalResult))
                {
                    if (decimalResult <= 500)
                    {
                        WriteLog($"Variavel foi limpa! Peso (estável) registrado: {decimalResult} ", LogLevel.Information);
                        return "0";
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(messureSIValue))
            {
                if (int.TryParse(messureSIValue, out int intResult))
                {
                    if (intResult <= 500)
                    {
                        WriteLog($"Variavel foi limpa! Peso (instável) registrado: {intResult} ", LogLevel.Information);
                        return "0";
                    }
                }
                else if (decimal.TryParse(messureSIValue, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentUICulture, out decimal decimalResult))
                {
                    if (decimalResult <= 500)
                    {
                        WriteLog($"Variavel foi limpa! Peso (instável) registrado: {decimalResult} ", LogLevel.Information);
                        return "0";
                    }
                }
            }

            //loop to measure stable weigth, comparing to first stable weigth
            for (int i = 0; i < validWeigthReadNeeded; i++)
            {
                rawData = await socketActiveHandler.ExecuteRequestAsync(RequestMessage);

                if (MessureIsValid(rawData) && rawData == firstRawData)
                {
                    validWeigthReaded++;
                }
                else
                {
                    //long unstable weigth
                    WriteLog($"Leitura invalida. Leitura atual: {rawData} - Buscando peso instável....", LogLevel.Information);
                    var unstableData = await socketActiveHandler.ExecuteRequestAsync(RetryRequestMessage);
                    WriteLog($"Peso (instável): {unstableData} ", LogLevel.Information);

                    //if unstable, stop the process and try again on next time
                    break;
                }

                //Sleeps to wait for REALLY stable weight
                Thread.Sleep(200);
            }

            //check for consecutives valid read AND for a valid meassure
            if (validWeigthReaded == validWeigthReadNeeded && MessureIsValid(rawData))
            {
                return rawData;
            }

            return string.Empty;
        }

        private bool MessureIsValid(string rawData)
        {
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return false;
            }

            if (!rawData.StartsWith("S"))
            {
                LogMessureErrorMessage(rawData);
                return false;
            }

            if (rawData.ElementAtOrDefault(2) != 'S')
            {
                LogUnstableMessureMessage(rawData);
                return false;
            }

            return true;
        }

        private bool TryGetMessure(MachineData machineData)
        {
            var fieldWeigthLength = machineData.CapturedDataList.FirstOrDefault(x => x.Name == CapturedDataWeigthName);
            if (fieldWeigthLength == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(machineData.RmlRawData))
            {
                return false;
            }

            var messureValue = GetMessureValue(machineData.RmlRawData);
            WriteLog($"Peso estável lido: {messureValue} ", LogLevel.Information);
            fieldWeigthLength.TrySetValue(messureValue);
            return true;
        }

        /// <summary>
        /// LogUnstableMessureMessage método é chamda quando há erros de medida.
        /// </summary>
        /// <remarks>
        /// Existem quatro mensagens de erro diferentes. A identificação sempre é composta por dois
        ///     caracteres:
        ///     • ES - Erro de sintaxe: o terminal IND570 não reconheceu o comando recebido.
        ///     • ET – Erro de transmissão: a balança recebeu um comando com problema, como um erro
        ///     de paridade.
        ///     • EL - Erro de lógica: o comando é entendido, mas o parâmetro está incorreto.
        ///     • EI – Erro interno: o comando é entendido, mas não pode ser executado no momento.
        ///     • CR- Retorno de carro (ASCII 13 dec.)
        ///     • LF - Alimentação de linha (ASCII 10 dec.)
        /// </remarks>
        /// <param name="rawData"></param>
        private void LogMessureErrorMessage(string rawData)
        {
            var idMessage = rawData[..2];

            switch (idMessage)
            {
                case "ES":
                    WriteLog($"Erro de sintaxe: o terminal IND570 não reconheceu o comando recebido. medida: {rawData}", LogLevel.Warning);
                    break;
                case "ET":
                    WriteLog($"Erro de transmissão: a balança recebeu um comando com problema, como um erro. medida: {rawData}", LogLevel.Warning);
                    break;
                case "EL":
                    WriteLog($"Erro de lógica: o comando é entendido, mas o parâmetro está incorreto. medida: {rawData}", LogLevel.Warning);
                    break;
                case "EI":
                    WriteLog($"Erro interno: o comando é entendido, mas não pode ser executado no momento. medida: {rawData}", LogLevel.Warning);
                    break;
                case "CR":
                    WriteLog($"Retorno de carro (ASCII 13 dec.). medida: {rawData}", LogLevel.Warning);
                    break;
                case "LF":
                    WriteLog($"Alimentação de linha (ASCII 10 dec.). medida: {rawData}", LogLevel.Warning);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// LogUnstableMessureMessage método é chamda quando a balança response, mas com problemas na medida
        /// </summary>
        /// <remarks>
        ///     S _ S _ WeigthValue _ Unit – Valor de peso estável.
        ///     C-28 METTLER TOLEDOIND570 Guia do usuário 30205316 | 16 | 03/2021 Comunicação
        ///     S _ D _ WeigthValue _ Unit – Valor de peso não estável.
        ///     S _ I – O comando é entendido, mas não pode ser executado no momento. (O terminal IND570
        ///     está executando outro comando, como a tara.)
        ///     S _ + – IND570 na faixa de sobrecarga.
        ///     S _ + – IND570 na faixa de baixa carga.
        ///     
        ///     S _ S _ WeigthValue _ Unit – Valor de peso estável atual. Alteração de peso
        ///     S _ D _ WeigthValue _ Unit – Valor de peso não estável.
        ///     S _ S _ WeigthValue _ Unit – Próximo valor de peso estável.
        ///     S_I – O comando é entendido, mas não pode ser executado neste momento. (O terminal IND570
        ///     está executando outro comando, como tara, ou timeout porque a estabilidade não foi obtida.)
        ///     S_L – Comando entendido, parâmetro errado
        ///     S _ + – IND570 na faixa de sobrecarga.
        ///     S _ + – IND570 na faixa de baixa carga.
        /// </remarks>
        /// <param name="rawData"></param>
        private void LogUnstableMessureMessage(string rawData)
        {
            var messureStatus = rawData.Substring(2, 1);

            switch (messureStatus)
            {
                case "D":
                    WriteLog($"Valor de peso não estável. medida: {rawData}", LogLevel.Warning);
                    break;
                case "I":
                    WriteLog($"O comando é entendido, mas não pode ser executado no momento. (O terminal IND570 está executando outro comando, como a tara.)", LogLevel.Warning);
                    break;
                case "+":
                    WriteLog($"IND570 na faixa de sobrecarga. medida: {rawData}", LogLevel.Warning);
                    break;
                case "-":
                    WriteLog($"IND570 na faixa de baixa carga. medida: {rawData}", LogLevel.Warning);
                    break;
                default:
                    break;
            }
        }

        private string GetMessureValue(string rawData)
        {
            var matches = Regex.Matches(rawData, @".*?([-]{0,1} *\d+.\d+)");

            return matches[0]?.Groups[1]?.Value?.Trim();
        }

        public override void Dispose()
        {
        }
    }
}