using System;
using System.Collections.Generic;

namespace Klabin.Rml.ClientLogic
{
    public class HistoryMachineDataRequest
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

    public class HistoryMachineDataResponse : ApiResponseBody<List<HistoryMachineDataResponseDto>>
    {

    }

    public class HistoryMachineDataResponseDto
    {
        public string DriverType { get; set; }
        public string MachineNumber { get; set; }
        public string RollNumber { get; set; }
        public string CutNumber { get; set; }
        public DateTime ReadTime { get; set; }
        public DateTime? SyncTime { get; set; }
        public DateTime? RollDate { get; set; }
        public string RmlRawData { get; set; }
        public bool Synchronized { get; set; }
        public string SynchronizedString
        {
            get
            {
                return Synchronized ? "Sim" : "Não";
            }
        }
        public string MeasureType { get; set; }
        public string MeasureValue { get; set; }

        public List<MachineCapturedData> capturedDataList { get; set; }

        public List<HistoryMachineDataResponseDto> ExtractMeasureValues()
        {
            var result = new List<HistoryMachineDataResponseDto>();
            if (capturedDataList == null)
                return result;

            foreach (var item in capturedDataList)
            {
                result.Add(new()
                {
                    DriverType = this.DriverType,
                    MachineNumber = this.MachineNumber,
                    RollNumber = this.RollNumber,
                    CutNumber = this.CutNumber,
                    ReadTime = this.ReadTime,
                    SyncTime = this.SyncTime,
                    RollDate = this.RollDate,
                    RmlRawData = this.RmlRawData,
                    MeasureType = item.DescriptionName,
                    MeasureValue = item.Value?.ToString()
                });
            }

            return result;
        }
    }



}
