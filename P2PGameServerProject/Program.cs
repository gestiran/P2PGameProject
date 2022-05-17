using System.Net;
using System.Text;
using P2PGameServerProject.Messages;
using P2PGameServerProject.Listeners;
using P2PGameServerProject.Users;
using P2PGameServerProject.Messages.ServerMessages;
using P2PGameServerProject.Rooms;

namespace P2PGameServerProject {
    internal partial class Program {
        private static Dictionary<UserIPKey, UserStatus> _users;
        private static GameRoom[] _rooms;
        private static ServerResponseBuilder _serverResponseBuilder;
        
        public static void Main(string[] args) {
            _users = new Dictionary<UserIPKey, UserStatus>();
            CreateRooms(10, out _rooms);
            _serverResponseBuilder = new ServerResponseBuilder();
            
            Console.WriteLine("Server started");
            TcpListener srv = new TcpListener(50900, HandleData);
            srv.StartListening();
        }

        private static void CreateRooms(int count, out GameRoom[] rooms) {
            rooms = new GameRoom[count];
            for (byte roomId = 0; roomId < rooms.Length; roomId++) rooms[roomId] = new GameRoom(RoomStatus.Empty);
        }

        private static bool HandleData(IPAddress address, byte[] data, out byte[] result) {
            switch ((ServerCommand)data[0]) {
                case ServerCommand.AddMeToConnected: return AddMeToConnectedHandle(address, data, out result);
                case ServerCommand.RemoveMeFromConnected: return RemoveMeFromConnectedHandle(address, data, out result);
                case ServerCommand.SetStatus: return SetStatusHandle(address, data, out result);
                case ServerCommand.GetFreeRoom: return GetFreeRoomHandle(address, data, out result);
                case ServerCommand.CreateRoom: return CreateRoomHandle(address, data, out result);
            }

            result = new byte[0];
            return false;
        }

        private static bool AddMeToConnectedHandle(IPAddress address, byte[] data, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                result = _serverResponseBuilder.AddMeToConnected(ActionResult.Failed);
                Console.WriteLine($"[{address}] AddMeToConnected - Failed");
            } else {
                _users.Add(userKey, UserStatus.Online);
                result = _serverResponseBuilder.AddMeToConnected(ActionResult.Success);
                Console.WriteLine($"[{address}] AddMeToConnected - Success");
            }
            return true;
        }

        private static bool RemoveMeFromConnectedHandle(IPAddress address, byte[] data, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                _users.Remove(userKey);
                result = _serverResponseBuilder.RemoveMeFromConnected(ActionResult.Success);
                Console.WriteLine($"[{address}] RemoveMeFromConnected - Success");
            } else {
                result = _serverResponseBuilder.RemoveMeFromConnected(ActionResult.Failed);
                Console.WriteLine($"[{address}] RemoveMeFromConnected - Failed :: Not find user!");
            }
            return true;
        }

        private static bool SetStatusHandle(IPAddress address, byte[] data, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                _users[userKey] = (UserStatus)data[1];
                result = _serverResponseBuilder.SetStatus(ActionResult.Success);
                Console.WriteLine($"[{address}] SetStatus - Success :: New status is {(UserStatus)data[1]}");
            } else {
                result = _serverResponseBuilder.SetStatus(ActionResult.Failed);
                Console.WriteLine($"[{address}] SetStatus - Failed :: Not find user!");
            }
            return true;
        }

        private static bool GetFreeRoomHandle(IPAddress address, byte[] data, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                int freeId = 0;
                bool isFinded = false;

                for (byte roomId = 0; roomId < _rooms.Length; roomId++) {
                    if (_rooms[roomId].status.Equals(RoomStatus.Lobby)) {
                        freeId = roomId;
                        isFinded = true;
                        break;
                    }
                }

                if (isFinded) {
                    result = _serverResponseBuilder.GetFreeRoom(ActionResult.Success, freeId);
                    Console.WriteLine($"[{address}] GetFreeRoom - Success :: Free room is {freeId}");
                } else {
                    result = _serverResponseBuilder.GetFreeRoom(ActionResult.Failed, 0);
                    Console.WriteLine($"[{address}] GetFreeRoom - Failed :: Free room is not finded!");
                }
            } else {
                result = _serverResponseBuilder.GetFreeRoom(ActionResult.Failed, 0);
                Console.WriteLine($"[{address}] GetFreeRoom - Failed :: Not find user!");
            }
            return true;
        }

        private static bool CreateRoomHandle(IPAddress address, byte[] data, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                int emptyRoom = 0;
                bool isFinded = false;

                for (byte roomId = 0; roomId < _rooms.Length; roomId++) {
                    if (_rooms[roomId].status.Equals(RoomStatus.Empty)) {
                        emptyRoom = roomId;
                        isFinded = true;
                        break;
                    }
                }

                if (isFinded) {
                    _rooms[emptyRoom].status = RoomStatus.Lobby;
                    _rooms[emptyRoom].host = userKey;
                    result = _serverResponseBuilder.CreateRoom(ActionResult.Success, emptyRoom);
                    Console.WriteLine($"[{address}] CreateRoom - Success :: Created room on {emptyRoom} id");
                } else {
                    result = _serverResponseBuilder.CreateRoom(ActionResult.Failed, 0);
                    Console.WriteLine($"[{address}] CreateRoom - Failed :: Empty room is not finded!");
                }
            } else {
                result = _serverResponseBuilder.CreateRoom(ActionResult.Failed, 0);
                Console.WriteLine($"[{address}] CreateRoom - Failed :: Not find user!");
            }
            return true;
        }
    }
}