using System;

namespace P2PGameServerProject.Messages.ServerMessages {
    public class ServerResponseBuilder {
        public byte[] AddMeToConnected(ActionResult result) => new[] {
            (byte) result
        };

        public byte[] RemoveMeFromConnected(ActionResult result) => new[] {
            (byte) result
        };

        public byte[] SetStatus(ActionResult result) => new[] {
            (byte) result
        };

        public byte[] GetFreeRoom(ActionResult result, int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new[] {
                (byte) result,
                roomIdBytes[0],
                roomIdBytes[1],
                roomIdBytes[2],
                roomIdBytes[3]
            };
        }

        public byte[] CreateRoom(ActionResult result, int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new[] {
                (byte) result,
                roomIdBytes[0],
                roomIdBytes[1],
                roomIdBytes[2],
                roomIdBytes[3]
            };
        }

        public byte[] RemoveRoom(ActionResult result) => new[] {
            (byte) result
        };
    }
}