using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Klabin.Rml.ClientLogic.SocketHandlers
{
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] Buffer { get; set; }

        // Received data string.
        public StringBuilder StringBuilder { get; set; }

        // Client socket.
        public Socket WorkSocket { get; set; }

        public ManualResetEvent ReceiveDone { get; set; }

        public StateObject()
        {
            Buffer = new byte[BufferSize];
            StringBuilder = new StringBuilder();
            WorkSocket = null;
        }
    }
}
