using System;
using System.Net;
using P2PGameServerProject.Users;

namespace P2PGameServerProject.Messages.ServerMessages {
    public class ServerRequestBuilder {
        public byte[] AddMeToConnected(IPAddress address) {
            byte[] addressBytes = address.GetAddressBytes();
            return new[] {
                (byte) ServerCommand.AddMeToConnected,
                addressBytes[0],
                addressBytes[1],
                addressBytes[2],
                addressBytes[3]
            };
        }

        public byte[] RemoveMeFromConnected() => new[] {
            (byte) ServerCommand.RemoveMeFromConnected
        };

        public byte[] SetStatus(UserStatus newStatus) => new[] {
            (byte) ServerCommand.SetStatus,
            (byte) newStatus
        };

        public byte[] GetFreeRoom() => new[] {
            (byte) ServerCommand.GetFreeRoom
        };

        public byte[] CreateRoom() => new[] {
            (byte) ServerCommand.CreateRoom
        };

        public byte[] RemoveRoom(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new[] {
                (byte) ServerCommand.RemoveRoom,
                roomIdBytes[0],
                roomIdBytes[1],
                roomIdBytes[2],
                roomIdBytes[3]
            };
        }
    }
}