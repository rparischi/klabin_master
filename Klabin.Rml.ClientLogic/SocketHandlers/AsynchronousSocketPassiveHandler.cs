using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Klabin.Rml.ClientLogic.SocketHandlers
{
    public class AsynchronousSocketPassiveHandler : AsynchronousSocketHandler
    {
        // Thread signal event
        private static ManualResetEvent threadSignal;
        private readonly Action<string> _notifyDataReceived;
        private Socket listener;
        private bool disposing;

        public AsynchronousSocketPassiveHandler(Action<string> notifyDataReceived, Action<string, Exception, LogLevel> writeLogFunc, CancellationToken cancellationToken)
            : base(writeLogFunc, cancellationToken)
        {
            threadSignal = new ManualResetEvent(false);
            _notifyDataReceived = notifyDataReceived;
        }

        public void StartListening(TcpReaderConfig config)
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ipAddress = IPAddress.Parse(config.Address);
            var remoteEndpoint = new IPEndPoint(ipAddress, config.Port);

            //set configs
            listener.ReceiveTimeout = config.ReadTimeout;
            listener.SendTimeout = config.WriteTimeout;

            _writeLogFunc("Configurando client...", null, LogLevel.Debug);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(remoteEndpoint);
                listener.Listen(100);

                // loop until is asked to stop
                while (!_cancellationToken.IsCancellationRequested)
                {
                    // Set the event to nonsignaled state.  
                    threadSignal.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    _writeLogFunc("Aguardando conexão...", null, LogLevel.Debug);

                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    //check cancellation token
                    if (_cancellationToken.IsCancellationRequested)
                        break;

                    // Wait until a connection is made before continuing.  
                    threadSignal.WaitOne();
                }
            }
            catch (Exception ex)
            {
                _writeLogFunc($"Erro no processo de escuta do Socket. Erro: {ex.Message}", ex, LogLevel.Error);
            }

            // Stop the socket server
            // this will end the socker communication
            Stop();
        }

        public void Stop()
        {
            _writeLogFunc("Fechando conexão...", null, LogLevel.Debug);

            // block concurrent dispose calls form multiple threads
            if (disposing)
                return;

            disposing = true;
            try
            {
                if (listener == null)
                    return;

                //listener.Shutdown(SocketShutdown.Both);
                listener.Close(1);
            }
            catch (Exception)
            {
            }
        }

        public void AcceptCallback(IAsyncResult result)
        {
            if (result == null)
            {
                return;
            }

            try
            {
                // Signal the main thread to continue.  
                threadSignal.Set();

                // Get the socket that handles the client request.  
                var listener = (Socket)result.AsyncState;

                //check if cancel is requested or if diposing
                if (_cancellationToken.IsCancellationRequested || disposing)
                {
                    return;
                }

                var handler = listener.EndAccept(result);

                // Create the state object.  
                var state = new StateObject
                {
                    WorkSocket = handler
                };

                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch (ObjectDisposedException)
            {
                //silence
            }
            catch (Exception ex)
            {
                _writeLogFunc("Erro ao receber os dados do listner", ex, LogLevel.Error);
            }
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // abort if cancellation have been request by a parent Thread
            if (_cancellationToken.IsCancellationRequested)
                return;

            string content = string.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            var state = (StateObject)ar.AsyncState;
            var handler = state.WorkSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.StringBuilder.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

                // get content as string
                content = state.StringBuilder.ToString();

                // clear string contect buffer
                state.StringBuilder.Clear();

                _writeLogFunc($"Read {content.Length} bytes from socket. Data: {content}", null, LogLevel.Trace);

                // Notify about the data received by the listner
                _notifyDataReceived(content);

                // Echo the data back to the client.  
                Send(handler, content);

                // Not all data received. Get more.  
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
        }

        public override void Dispose()
        {
            Stop();

            listener.Dispose();
            listener = null;
        }
    }
}
