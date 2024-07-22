using System;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic
{
    /// <summary>
    /// Behaviour of machine readers
    /// </summary>
    interface IMachineReader : IObservable<MachineData>
    {
        public Task<MachineData> GetMachineData();
        public string GetReaderLog();
        public bool HasSuccessfullyInitialized();
    }
}
