using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.SocketHandlers
{
    public class AsynchronousSocketActiveHandler : AsynchronousSocketHandler
    {
        private readonly TcpReaderConfig _config;
        private bool disposing;

        public AsynchronousSocketActiveHandler(Action<string, Exception, LogLevel> writeLogFunc, TcpReaderConfig config, CancellationToken cancellationToken)
            : base(writeLogFunc, cancellationToken)
        {
            _config = config;
        }

        public async Task<string> ExecuteRequestAsync(string request)
        {
            var socket = InitializeSocket(_config);
            _writeLogFunc("Configurando client...", null, LogLevel.Trace);

            var remoteEndpoint = CreateRemoteEndpoint(_config);
            await ConnectSocketAsync(socket, _config, remoteEndpoint, _cancellationToken);

            Send(socket, request);
            var result = ReceiveData(socket, _cancellationToken);

            CloseConnection(socket);
            return result;
        }

        private async Task ConnectSocketAsync(Socket socket, TcpReaderConfig config, IPEndPoint remoteEndpoint, CancellationToken cancellationToken)
        {
            var count = 0;
            while (!cancellationToken.IsCancellationRequested && !socket.Connected && count <= 5)
            {
                count++;
                try
                {
                    await socket.ConnectAsync(remoteEndpoint, cancellationToken);
                }
                catch (Exception ex)
                {
                    _writeLogFunc($"Erro ao se conectar no endereço {config.Address}, e na porta {config.Port}", ex, LogLevel.Error);
                }
            }
        }

        private string ReceiveData(Socket socket, CancellationToken cancellationToken)
        {
            // abort if cancellation have been request by a parent Thread
            if (cancellationToken.IsCancellationRequested)
            {
                return string.Empty;
            }

            try
            {
                // Create the state object.  
                StateObject state = new();
                state.WorkSocket = socket;
                state.ReceiveDone = new(false);

                // Begin receiving the data from the remote device.
                socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                state.ReceiveDone.WaitOne(_config.ReadTimeout);

                return state.StringBuilder?.ToString();
            }
            catch (Exception ex)
            {
                _writeLogFunc($"Erro no processo de escuta do Socket. Erro: {ex.Message}", ex, LogLevel.Error);
                return string.Empty;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (disposing)
                {
                    return;
                }

                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                var state = (StateObject)ar.AsyncState;
                var client = state.WorkSocket;

                if (!client.Connected)
                {
                    return;
                }

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.StringBuilder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    return;
                }

                state.ReceiveDone.Set();
            }
            catch (Exception ex)
            {
                _writeLogFunc($"Erro no processo de escuta do Socket. Erro: {ex.Message}", ex, LogLevel.Error);
            }
        }

        private IPEndPoint CreateRemoteEndpoint(TcpReaderConfig config)
        {
            var ipAddress = IPAddress.Parse(config.Address);
            return new IPEndPoint(ipAddress, config.Port);
        }

        private Socket InitializeSocket(TcpReaderConfig config)
        {
            return new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = config.ReadTimeout,
                SendTimeout = config.WriteTimeout
            };
        }

        public void CloseConnection(Socket socket)
        {
            _writeLogFunc("Fechando conexão...", null, LogLevel.Trace);

            try
            {
                if (socket == null)
                {
                    return;
                }

                socket.Close(1);
            }
            catch (Exception)
            {
            }
        }

        public override void Dispose()
        {
            disposing = true;
        }
    }
}
