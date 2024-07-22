using System;

namespace Klabin.Rml.ClientLogic
{
    public class LastMachineDataRequest
    {
        public string MachineNumber { get; set; }
        public string RollNumber { get; set; }
        public string CutNumber { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public DateTime? InitialRollDate { get; set; }
        public DateTime? FinalRollDate { get; set; }
        public string DriverType { get; set; }
        public string MeasureType { get; set; }
        public bool? Synchronized { get; set; }
    }

    public class LastMachineDataResponse : ApiResponseBody<HistoryMachineDataResponseDto>
    {

    }
}
