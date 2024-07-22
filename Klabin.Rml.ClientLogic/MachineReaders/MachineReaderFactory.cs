using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Klabin.Rml.ClientLogic.MachineReaders
{
    public class MachineReaderFactory
    {
        /// <summary>
        /// Creates a specific machine reader but returns the base abstract type
        /// </summary>
        /// <param name="readerConfig"></param>
        /// <param name="logLevel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static ReaderBase CreateMachineReader(ReaderConfig readerConfig, LogLevel logLevel, ILogger logger, CancellationToken cancellationToken)
        {
            return readerConfig.MachineType switch
            {
                MachineType.ReelWeigth => CreateMachineActiveReader(readerConfig, logLevel, logger, cancellationToken),
                MachineType.ReelWeightRinnert => CreateMachineActiveReader(readerConfig, logLevel, logger, cancellationToken),
                MachineType.ReelWeightToledo => CreateMachineActiveReader(readerConfig, logLevel, logger, cancellationToken),
                MachineType.ReelWeigthP08 => CreateMachineActiveReaderP08(readerConfig, logLevel, logger, cancellationToken),
                MachineType.ReelLength => CreateMachinePassiveReader(readerConfig, logLevel, logger, cancellationToken),
                _ => default,
            };
        }

        private static ReaderBase CreateMachineActiveReaderP08(ReaderConfig readerConfig, LogLevel logLevel, ILogger logger, CancellationToken cancellationToken)
        {
            return readerConfig.ReaderType switch
            {
                ReaderType.TCP => new TCP_MachineActiveReaderP08(readerConfig as TcpReaderConfig, logLevel, logger, cancellationToken),
                ReaderType.SERIAL => new Serial_COM_weightReader(readerConfig as SerialReaderConfig, logLevel, logger, cancellationToken),
                _ => default,
            };
        }

        private static ReaderBase CreateMachineActiveReader(ReaderConfig readerConfig, LogLevel logLevel, ILogger logger, CancellationToken cancellationToken)
        {
            return readerConfig.ReaderType switch
            {
                ReaderType.TCP => new TCP_MachineActiveReader(readerConfig as TcpReaderConfig, logLevel, logger, cancellationToken),
                ReaderType.SERIAL => new Serial_COM_weightReader(readerConfig as SerialReaderConfig, logLevel, logger, cancellationToken),
                _ => default,
            };
        }

        private static ReaderBase CreateMachinePassiveReader(ReaderConfig readerConfig, LogLevel logLevel, ILogger logger, CancellationToken cancellationToken)
        {
            return readerConfig.ReaderType switch
            {
                ReaderType.SERIAL => new Serial_COM_lengthReader(readerConfig as SerialReaderConfig, logLevel, logger, cancellationToken),
                ReaderType.TCP => new TCP_MachineReader(readerConfig as TcpReaderConfig, logLevel, logger, cancellationToken),
                _ => default,
            };
        }
    }
}
