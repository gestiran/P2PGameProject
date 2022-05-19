using System.Net;
using UnityEngine;

namespace P2PGameClientProject.Network.Data {
    public static class ServerData {
        public static IPAddress serverIp => _serverIp;
        public static int serverPort => _serverPort;

        private static IPAddress _serverIp;
        private static int _serverPort;

        static ServerData() {
            ServerDataContainer dataContainer = Resources.Load<ServerDataContainer>("ServerDataContainer");
            _serverIp = IPAddress.Parse(dataContainer.address);
            _serverPort = dataContainer.port;
        }

        public static void ChangeServerAddress(IPAddress address, int port) {
            _serverIp = address;
            _serverPort = port;
        }
    }
}