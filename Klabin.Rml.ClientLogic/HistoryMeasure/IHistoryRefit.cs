using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.HistoryMeasure
{
    interface IHistoryRefit
    {
        [Get("")]
        Task<HistoryMachineDataResponse> GetMachineHistoryMachineMeasure(HistoryMachineDataRequest request);

        [Get("/last")]
        Task<LastMachineDataResponse> GetLastMachineDataResponse(LastMachineDataRequest request);
    }
}
