using Microsoft.Extensions.Logging;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Klabin.Rml.ClientLogic.SocketHandlers
{
    public abstract class AsynchronousSocketHandler : IDisposable
    {
        protected readonly CancellationToken _cancellationToken;
        protected readonly Action<string, Exception, LogLevel> _writeLogFunc;

        public AsynchronousSocketHandler(Action<string, Exception, LogLevel> writeLogFunc, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _writeLogFunc = writeLogFunc;
        }

        protected void Send(Socket handler, string data)
        {
            // abort if cancellation have been request by a parent Thread
            if (_cancellationToken.IsCancellationRequested)
            {
                return;
            }

            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        protected void SendCallback(IAsyncResult result)
        {
            // abort if cancellation have been request by a parent Thread
            if (_cancellationToken.IsCancellationRequested)
                return;

            try
            {
                // Retrieve the socket from the state object.  
                var handler = (Socket)result.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(result);
            }
            catch (Exception ex)
            {
                _writeLogFunc($"Erro ao enviar o echo da mensagem para o client", ex, LogLevel.Error);
            }
        }

        public abstract void Dispose();
    }
}
