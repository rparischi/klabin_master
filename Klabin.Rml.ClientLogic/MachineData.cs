using System;
using System.Collections.Generic;
using System.Globalization;

namespace Klabin.Rml.ClientLogic
{
    /// <summary>
    /// Represents values and data read/obtained from the Machine
    /// </summary>
    public class MachineData
    {
        public string MachineNumber { get; set; }
        public string RollNumber { get; set; }
        public string CutNumber { get; set; }
        public DateTime ReadTime { get; set; }
        public DateTime RollDate { get; set; }
        public string RmlRawData { get; set; }
        public string OperationLog { get; set; }
        public string DriverType { get; set; }
        public string MachineType { get; set; }
        public List<MachineCapturedData> CapturedDataList { get; set; }

        public MachineData()
        {
            CapturedDataList = new List<MachineCapturedData>();
        }

        public MachineData(string machineNumber, MachineType machineType, List<CapturedDataConfig> capturedDataConfigs)
        {
            //init with config names and positions
            CapturedDataList = new List<MachineCapturedData>(capturedDataConfigs.Count);
            foreach (var config in capturedDataConfigs)
            {
                CapturedDataList.Add(new(config));
            }

            MachineNumber = machineNumber;
            MachineType = machineType.ToString();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(RmlRawData);
        }
    }

    public enum MachineCapturedDataType
    { 
        Int,
        Decimal
    }

    /// <summary>
    /// Config of data to be captured
    /// </summary>
    public class CapturedDataConfig
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public string DescriptionName { get; set; }
        public MachineCapturedDataType DataType { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Name);
        }
    }

    /// <summary>
    /// Values captured from the Machine
    /// </summary>
    public class MachineCapturedData : CapturedDataConfig
    {
        public object Value { get; set; }

        public MachineCapturedData()
        {

        }

        public MachineCapturedData(CapturedDataConfig config)
        {
            DataType = config.DataType;
            Position = config.Position;
            DescriptionName = config.DescriptionName;
            Name = config.Name;
        }


        public bool TrySetValue(string value)
        {
            try
            {
                switch (DataType)
                {
                    case MachineCapturedDataType.Int:
                        if (int.TryParse(value, out int intResult))
                        {
                            Value = intResult;
                        }
                        break;
                    case MachineCapturedDataType.Decimal:
                        if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentUICulture, out decimal decimalResult))
                        {
                            Value = decimalResult;
                        }
                        break;
                    default:
                        return false;
                }

                if (Value == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                //silence
                return false;
            }
        }

        public bool CompareFieldValueLessThan(object anotherValue)
        {
            //try easy mode
            if (Value == anotherValue)
                return true;

            if (Value == null && anotherValue == null)
                return true;

            if (Value == null)
                return false;
           
            switch (DataType)
            {
                case MachineCapturedDataType.Int:
                    return (int)Value < Convert.ToInt32(anotherValue);
                case MachineCapturedDataType.Decimal:
                    return (decimal)Value < Convert.ToDecimal(anotherValue);
            }

            return false;
        }

        public bool CompareFieldValueGreaterThan(object anotherValue)
        {
            //try easy mode
            if (Value == anotherValue)
                return true;

            if (Value == null && anotherValue == null)
                return true;

            if (Value == null)
                return false;

            switch (DataType)
            {
                case MachineCapturedDataType.Int:
                    return (int)Value > Convert.ToInt32(anotherValue);
                case MachineCapturedDataType.Decimal:
                    return (decimal)Value > Convert.ToDecimal(anotherValue);
            }

            return false;
        }

    }
}
