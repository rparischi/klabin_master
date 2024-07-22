using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic
{
    /// <summary>
    /// A common reader logic to every machine
    /// </summary>
    public abstract class ReaderBase : IMachineReader, IDisposable
    {
        private readonly ILogger _logger;

        private StringBuilder logBuilder;
        protected List<IObserver<MachineData>> observers;
        private readonly LogLevel _minLogLevel;
        protected bool successfullyInitialized;
        protected readonly CancellationToken _cancellationToken;

        public ReaderBase(LogLevel minLogLevel, ILogger logger, CancellationToken token)
        {
            _logger = logger;

            logBuilder = new StringBuilder();
            observers = new List<IObserver<MachineData>>(2);
            _minLogLevel = minLogLevel;
            _cancellationToken = token;
        }

        protected void WriteLog(string message, LogLevel logLevel = LogLevel.Information)
        {
            if (logLevel >= _minLogLevel)
            {
                logBuilder.AppendLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {message}");
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
                logBuilder.AppendLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - ERROR: {message} - EX: {ex}");
            }

            _logger.Log(logLevel, ex, message);
            ClearLogBuilder();
        }

        /// <summary>
        /// Implement interface abstractally
        /// </summary>
        /// <returns></returns>
        public abstract Task<MachineData> GetMachineData();

        /// <summary>
        /// Provides a way to observers to Subscribe to this reader
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<MachineData> observer)
        {
            observers.Add(observer);

            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<MachineData>(observers, observer);
        }

        protected void NotifyObservers(MachineData machineData)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(machineData);
            }
        }

        /// <summary>
        /// Indicates if the reader has initialized its low-level communication with success
        /// </summary>
        /// <returns></returns>
        public bool HasSuccessfullyInitialized()
        {
            return successfullyInitialized;
        }

        /// <summary>
        /// Get Readers current log stash
        /// </summary>
        /// <returns></returns>
        public string GetReaderLog()
        {
            return logBuilder.ToString();
        }

        /// <summary>
        /// Não deixa que o logbuilder se torne muito grande
        /// </summary>
        /// <param name="force"></param>
        private void ClearLogBuilder(bool force = false)
        {
            if (logBuilder != null)
            {
                if (force || logBuilder.Length > 3000)
                {
                    logBuilder.Clear();
                }
            }
        }

        public abstract void Dispose();


    }
}
