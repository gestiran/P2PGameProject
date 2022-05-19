using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace P2PGameClientProject.Network.HostSystem {
    public class HostListener {
        
        public bool isListening => _isListening;
        
        private IPEndPoint _ipEndPoint;
        private Socket _socket;
        private TryDataHandle _tryDataHandle;
        private bool _isListening;

        public HostListener(int port, TryDataHandle tryDataHandle) {
            _ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            _tryDataHandle = tryDataHandle;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async void StartListeningAsync() {
            Task listening = Task.Run(StartListeningAsync);
            while (_isListening) await Task.Delay(32);
            listening.Dispose();
        }
        
        public async void ListeningAsync() {
            if (_isListening) return;
            _isListening = true;
            _socket.Bind(_ipEndPoint);
            _socket.Listen(1);

            while (_isListening) {
                Socket listener = _socket.Accept();

                if (listener.RemoteEndPoint is IPEndPoint clientPoint) {
                    List<byte> data = new List<byte>();
                    byte[] buffer = new byte[256];
                    int size;

                    do {
                        size = listener.Receive(buffer);
                        for (byte bufferId = 0; bufferId < size; bufferId++) data.Add(buffer[bufferId]);
                    } while (listener.Available > 0);

                    if (_tryDataHandle(clientPoint.Address, data.ToArray(), out byte[] result)) listener.Send(result);
                }

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
                await Task.Delay(16);
            }
        }
        
        public void StopListening() => _isListening = false;
    }
}