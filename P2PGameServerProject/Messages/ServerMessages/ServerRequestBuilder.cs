using System.Net;
using P2PGameServerProject.Users;

namespace P2PGameServerProject.Messages.ServerMessages {
    public class ServerRequestBuilder {
        public byte[] AddMeToConnected(IPAddress address) {
            byte[] addressBytes = address.GetAddressBytes();
            return new byte[] { (byte)ServerCommand.AddMeToConnected, addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3] };
        }

        public byte[] RemoveMeFromConnected() => new byte[] { (byte)ServerCommand.RemoveMeFromConnected };

        public byte[] SetStatus(UserStatus newStatus) => new byte[] { (byte)ServerCommand.SetStatus, (byte)newStatus };

        public byte[] GetFreeRoom() => new byte[] { (byte)ServerCommand.GetFreeRoom };

        public byte[] CreateRoom() => new byte[] { (byte)ServerCommand.CreateRoom };

        public byte[] RemoveRoom(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new byte[] { (byte)ServerCommand.RemoveRoom, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3] };
        }

        public byte[] SendMatchResult(int roomId, byte result) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new byte[] { (byte)ServerCommand.RemoveRoom, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3], result };
        }
    }
}