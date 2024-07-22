using Klabin.Rml.ClientLogic.HistoryMeasure;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Klabin.Rml.ClientLogic.MachineObservers
{
    public class ReelLength_Observer : MachineObserver
    {
        public ReelLength_Observer(ReaderConfig readerConfig, ReaderBase machineReader, ILogger logger, HistorySearchService historySearchService) : base(readerConfig, machineReader, logger, historySearchService)
        {
        }

        public override void OnNext(MachineData value)
        {
            machineDataReaded = value;

            //verify if the reel length has been acquired by the machine reader
            var lengthParameter = machineDataReaded.CapturedDataList.FirstOrDefault(x => x.Name == "Length");
            if (lengthParameter.Value != null &&
               lengthParameter.CompareFieldValueGreaterThan(100))
            {
                OnCompleted();
            }
        }
    }
}
