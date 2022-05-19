using System.Net;
using P2PGameClientProject.Network.Data;

namespace P2PGameClientProject.Network.ClientSystem {
    public static class GameClient {
        private static readonly Sender _toServerSender;
        private static Sender _toHostSender;

        static GameClient() => _toServerSender = new Sender(ServerData.serverIp, ServerData.serverPort);

        public static void SetHost(IPAddress address, int port) => _toHostSender = new Sender(address, port);
        
        public static void ChangeHost(IPAddress address, int port) => _toHostSender.ChangeRecipient(address, port);

        public static void ChangeServer(IPAddress address, int port) => _toServerSender.ChangeRecipient(address, port);
        
        public static void SendToServer(byte[] data, ResultCallback Callback) => _toServerSender.SendData(data, Callback);
        
        public static void SendToHost(byte[] data, ResultCallback Callback) => _toHostSender.SendData(data, Callback);
    }
}