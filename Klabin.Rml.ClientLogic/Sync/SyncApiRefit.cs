using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.Sync
{
    public interface SyncApiRefit
    {
        [Post("")]
        Task<HttpResponseMessage> SyncMachineData(MachineData machineData);
    }
}
