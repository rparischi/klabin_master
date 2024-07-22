using Microsoft.Extensions.Logging;
using Klabin.Rml.ClientLogic.HistoryMeasure;
using System.Linq;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.MachineObservers
{
    public class Weigth_Observer : MachineObserver
    {
        private readonly HistorySearchService _historySearchService;
        private readonly string _machineNumber;
        private object lastMachineSyncedValue;

        public Weigth_Observer(ReaderConfig readerConfig, ReaderBase machineReader, ILogger logger, HistorySearchService historySearchService) : base(readerConfig, machineReader, logger, historySearchService)
        {
            _historySearchService = historySearchService;
            _machineNumber = readerConfig.MachineNumber;
        }

        public override void OnNext(MachineData value)
        {
            machineDataReaded = value;
            var lengthParameter = machineDataReaded?.CapturedDataList?.FirstOrDefault(x => x.Name == "Weight");
            //if we have some returned value than check for history
            if (lengthParameter != null && lengthParameter.Value != null && lengthParameter.CompareFieldValueGreaterThan(0))
            {
                //if the last synced value is diferent of the current value, then we must sync it
                //else we can "ignore" the readed value and keep reading
                var lastSyncedValue = GetLastMachineSyncedReadAsync().ConfigureAwait(true).GetAwaiter().GetResult();
                if (lengthParameter.Value.ToString() != lastSyncedValue)
                {
                    //stores locally the last value
                    SetLastMachineSyncedValue(lengthParameter.Value);

                    //Notify observers
                    OnCompleted();
                }
            }
        }

        private void SetLastMachineSyncedValue(object value)
        {
            lastMachineSyncedValue = value;
        }


        private async Task<string> GetLastMachineSyncedReadAsync()
        {
            if (lastMachineSyncedValue != null)
            {
                return lastMachineSyncedValue.ToString();
            }

            var (model, success) = await _historySearchService.SearchLastMachineDataAsync(new()
            {
                MachineNumber = _machineNumber
            });

            if (success)
            {
                lastMachineSyncedValue = model?.capturedDataList[0]?.Value;
                return lastMachineSyncedValue?.ToString();
            }

            return string.Empty;
        }
    }
}
