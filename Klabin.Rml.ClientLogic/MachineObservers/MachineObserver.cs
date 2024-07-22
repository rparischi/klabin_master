using Klabin.Rml.ClientLogic.HistoryMeasure;
using Microsoft.Extensions.Logging;
using System;

namespace Klabin.Rml.ClientLogic.MachineObservers
{
    public abstract class MachineObserver : IObserver<MachineData>
    {
        internal IDisposable cancellation;
        internal readonly ReaderConfig _readerConfig;
        internal readonly ReaderBase _machineReader;
        internal bool hasCompleted;
        internal MachineData machineDataReaded;
        internal ILogger _logger;

        public MachineObserver(ReaderConfig readerConfig,  ReaderBase machineReader, ILogger logger, HistorySearchService historySearchService)
        {
            _readerConfig = readerConfig;
            _machineReader = machineReader;
            _logger = logger;
            Subscribe(_machineReader);
        }

        public virtual void OnCompleted()
        {
            hasCompleted = true;
        }

        public virtual void OnError(Exception error)
        {
            //write log
            _logger.LogError(error.Message, error);
        }

        public virtual void OnNext(MachineData value)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(value));
        }
        
        public virtual void Subscribe(ReaderBase provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
        }

        public virtual bool HasCompletedMachineRead()
        {
            return hasCompleted;
        }

        public virtual void ResetObserver()
        {
            hasCompleted = false;
            machineDataReaded = null;
        }

        public virtual MachineData GetLastData()
        {
            return machineDataReaded;
        }

    }

    public static class MachineObserverFactory
    {
        public static MachineObserver Create(ReaderConfig config, ReaderBase machineReader, ILogger logger, HistorySearchService historySearchService)
        {
            return config.MachineType switch
            {
                MachineType.ReelLength => new ReelLength_Observer(config, machineReader, logger, historySearchService),
                MachineType.ReelWeigth => new Weigth_Observer(config, machineReader, logger, historySearchService),
                MachineType.ReelWeightRinnert => new Weigth_Observer(config, machineReader, logger, historySearchService),
                MachineType.ReelWeightToledo => new Weigth_Observer(config, machineReader, logger, historySearchService),
                MachineType.ReelWeigthP08 => new Weigth_Observer(config, machineReader, logger, historySearchService),
                _ => default
            };
        }
    }
}
