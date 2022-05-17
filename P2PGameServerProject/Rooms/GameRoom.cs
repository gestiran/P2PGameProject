using P2PGameServerProject.Users;

namespace P2PGameServerProject.Rooms {
    public struct GameRoom : IEquatable<GameRoom> {
        public RoomStatus status;
        public UserIPKey host;
        public UserIPKey user;

        public GameRoom(RoomStatus status) {
            this.status = status;
            host = default;
            user = default;
        }

        public bool Equals(GameRoom other) => status.Equals(other.status) && host.Equals(other.host);
    }
}