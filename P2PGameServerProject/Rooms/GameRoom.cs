using System;
using P2PGameServerProject.Data;
#pragma warning disable 8610
#pragma warning disable 8614

namespace P2PGameServerProject.Rooms {
    public struct GameRoom : IEquatable<GameRoom> {
        public RoomStatus status;
        public UserIP host;
        public UserIP user;

        public GameRoom(RoomStatus status) {
            this.status = status;
            host = default;
            user = default;
        }

        public bool Equals(GameRoom other) => status.Equals(other.status) && host.Equals(other.host);
        
        public override bool Equals(object obj) {
            if (obj != null && obj is GameRoom gameRoom) return Equals(gameRoom);
            return false;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}