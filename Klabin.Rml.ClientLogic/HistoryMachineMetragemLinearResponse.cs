using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic
{
    public class HistoryLinearMeasureDataRequest
    {
        public string MachineNumber { get; set; }
        public string RollNumber { get; set; }
        public string CutNumber { get; set; }
        public DateTime RollDate { get; set; }
        public DateTime FinalRollDate { get; set; }
    }

    public class HistoryLinearMeasureDataResponseDto
    {
        public DateTime RollDate { get; set; }
        public string MachineNumber { get; set; }
        public string RollNumber { get; set; }
        public string CutNumber { get; set; }

    }
}
