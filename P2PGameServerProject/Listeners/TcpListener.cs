using System.Net;
using System.Net.Sockets;

namespace P2PGameServerProject.Listeners {
    public sealed class TcpListener {
        private IPEndPoint _ipEndPoint;
        private Socket _socket;
        private TryDataHandle _tryDataHandle;

        public TcpListener(int port, TryDataHandle tryDataHandle) {
            _ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            _tryDataHandle = tryDataHandle;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListening() {
            _socket.Bind(_ipEndPoint);
            _socket.Listen(1);

            while (true) {
                Socket listener = _socket.Accept();

                if (listener.RemoteEndPoint is IPEndPoint clientPoint) {
                    List<byte> data = new List<byte>();
                    byte[] buffer = new byte[256];
                    int size = 0;

                    do {
                        size = listener.Receive(buffer);
                        for (byte bufferId = 0; bufferId < size; bufferId++) data.Add(buffer[bufferId]);
                    } while (listener.Available > 0);

                    if (_tryDataHandle(clientPoint.Address, data.ToArray(), out byte[] result)) listener.Send(result);
                }

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}