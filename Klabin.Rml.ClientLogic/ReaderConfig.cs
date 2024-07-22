using System.Collections.Generic;
using System.IO.Ports;

namespace Klabin.Rml.ClientLogic
{
    public enum MachineType
    {
        ReelLength,
        ReelWeigth,
        ReelWeightRinnert,
        ReelWeightToledo,
        ReelWeigthP08
    }

    public enum ReaderType
    {
        SERIAL,
        TCP
    }

    public abstract class ReaderConfig
    {
        public string BaseApiUrl { get; set; }
        public string BaseApiSafariUrl { get; set; }
        public string MachineNumber { get; set; }
        public ReaderType ReaderType { get; set; }
        public int LookpUpWaitTime { get; set; }
        public MachineType MachineType { get; set; }
        public List<CapturedDataConfig> CapturedDataConfigList { get; set; }
        public bool DebugMode { get; set; }
        public int NumberOfDays { get; set; }

        public ReaderConfig()
        {
            CapturedDataConfigList = new List<CapturedDataConfig>(2);
        }
    }

    public class SerialReaderConfig : ReaderConfig
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int DataBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
    }

    public class TcpReaderConfig : ReaderConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
    }
}
