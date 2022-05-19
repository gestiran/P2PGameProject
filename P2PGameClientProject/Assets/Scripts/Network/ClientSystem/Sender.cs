using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace P2PGameClientProject.Network.ClientSystem {
    public class Sender {
        private IPAddress _address;
        private int _port;

        public Sender(IPAddress address, int port) {
            _address = address;
            _port = port;
        }

        public void ChangeRecipient(IPAddress address, int port) {
            _address = address;
            _port = port;
        }
        
        public void SendData(byte[] data, ResultCallback Callback) {
            IPEndPoint ipEndPoint = new IPEndPoint(_address, _port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipEndPoint);
            socket.Send(data);
        
            List<byte> receivedData = new List<byte>();
            byte[] buffer = new byte[256];
            int size;
        
            do {
                size = socket.Receive(buffer);
                for (int i = 0; i < size; i++) receivedData.Add(buffer[i]);
            }
            while (socket.Available > 0);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            
            Callback(receivedData.ToArray());
        }
    }
}