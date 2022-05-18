using System;
using System.Collections.Generic;
using System.Net;
using P2PGameServerProject.Messages;
using P2PGameServerProject.Messages.ServerMessages;
using P2PGameServerProject.Rooms;
using P2PGameServerProject.Users;

namespace P2PGameServerProject.Handlers {
    public class ServerCommandsHandler {
        private readonly Dictionary<UserIPKey, UserStatus> _users;
        private readonly GameRoom[] _rooms;
        
        private readonly ServerResponseBuilder _serverResponseBuilder;
        
        public ServerCommandsHandler(GameRoom[] rooms, Dictionary<UserIPKey, UserStatus> users) {
            _rooms = rooms;
            _users = users;
            _serverResponseBuilder = new ServerResponseBuilder();
        }
        
        public bool HandleData(IPAddress address, byte[] data, out byte[] result) {
            switch ((ServerCommand)data[0]) {
                case ServerCommand.AddMeToConnected: return AddMeToConnectedHandle(address, out result);
                case ServerCommand.RemoveMeFromConnected: return RemoveMeFromConnectedHandle(address, out result);
                case ServerCommand.SetStatus: return SetStatusHandle(address, data, out result);
                case ServerCommand.GetFreeRoom: return GetFreeRoomHandle(address, out result);
                case ServerCommand.CreateRoom: return CreateRoomHandle(address, out result);
                case ServerCommand.RemoveRoom: return RemoveRoomHandle(address, data, out result);
            }

            result = new byte[0];
            return false;
        }

        private bool AddMeToConnectedHandle(IPAddress address, out byte[] result) {
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

        private bool RemoveMeFromConnectedHandle(IPAddress address, out byte[] result) {
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

        private bool SetStatusHandle(IPAddress address, byte[] data, out byte[] result) {
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

        private bool GetFreeRoomHandle(IPAddress address, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                int freeId = 0;
                bool isFind = false;

                for (byte roomId = 0; roomId < _rooms.Length; roomId++) {
                    if (!_rooms[roomId].status.Equals(RoomStatus.Lobby)) continue;
                    freeId = roomId;
                    isFind = true;
                    break;
                }

                if (isFind) {
                    result = _serverResponseBuilder.GetFreeRoom(ActionResult.Success, freeId);
                    Console.WriteLine($"[{address}] GetFreeRoom - Success :: Free room is {freeId}");
                } else {
                    result = _serverResponseBuilder.GetFreeRoom(ActionResult.Failed, 0);
                    Console.WriteLine($"[{address}] GetFreeRoom - Failed :: Free room is not find!");
                }
            } else {
                result = _serverResponseBuilder.GetFreeRoom(ActionResult.Failed, 0);
                Console.WriteLine($"[{address}] GetFreeRoom - Failed :: Not find user!");
            }
            return true;
        }

        private bool CreateRoomHandle(IPAddress address, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                int emptyRoom = 0;
                bool isFind = false;

                for (byte roomId = 0; roomId < _rooms.Length; roomId++) {
                    if (!_rooms[roomId].status.Equals(RoomStatus.Empty)) continue;
                    emptyRoom = roomId;
                    isFind = true;
                    break;
                }

                if (isFind) {
                    _rooms[emptyRoom].status = RoomStatus.Lobby;
                    _rooms[emptyRoom].host = userKey;
                    result = _serverResponseBuilder.CreateRoom(ActionResult.Success, emptyRoom);
                    Console.WriteLine($"[{address}] CreateRoom - Success :: Created room on {emptyRoom} id");
                } else {
                    result = _serverResponseBuilder.CreateRoom(ActionResult.Failed, 0);
                    Console.WriteLine($"[{address}] CreateRoom - Failed :: Empty room is not find!");
                }
            } else {
                result = _serverResponseBuilder.CreateRoom(ActionResult.Failed, 0);
                Console.WriteLine($"[{address}] CreateRoom - Failed :: Not find user!");
            }
            return true;
        }

        private bool RemoveRoomHandle(IPAddress address, byte[] data, out byte[] result) {
            UserIPKey userKey = new UserIPKey(address);
            if (_users.ContainsKey(userKey)) {
                int roomId = BitConverter.ToInt32(data, 1);

                if (roomId < _rooms.Length) {
                    if (_rooms[roomId].status.Equals(RoomStatus.Lobby)) {
                        if (_rooms[roomId].host.Equals(userKey)) {
                            _rooms[roomId] = new GameRoom(RoomStatus.Empty);
                            result = _serverResponseBuilder.RemoveRoom(ActionResult.Success);
                            Console.WriteLine($"[{address}] RemoveRoom - Success :: Room on {roomId} is removed");
                        } else {
                            result = _serverResponseBuilder.RemoveRoom(ActionResult.Failed);
                            Console.WriteLine($"[{address}] RemoveRoom - Success :: You is not host on this room");
                        }
                    } else {
                        result = _serverResponseBuilder.RemoveRoom(ActionResult.Failed);
                        Console.WriteLine($"[{address}] RemoveRoom - Success :: Room with id {roomId} is {_rooms[roomId].status} but not Lobby");
                    }
                } else {
                    result = _serverResponseBuilder.RemoveRoom(ActionResult.Failed);
                    Console.WriteLine($"[{address}] RemoveRoom - Success :: Room with id {roomId} is not found");
                }
            } else {
                result = _serverResponseBuilder.RemoveRoom(ActionResult.Failed);
                Console.WriteLine($"[{address}] RemoveRoom - Failed :: Not find user!");
            }
            return true;
        }
    }
}