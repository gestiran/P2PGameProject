using System;
using System.Collections.Generic;
using P2PGameServerProject.Data;
using P2PGameServerProject.Handlers;
using P2PGameServerProject.Listeners;
using P2PGameServerProject.Rooms;
#pragma warning disable 8618

namespace P2PGameServerProject {
    public class Program {
        private static Dictionary<UserIP, UserStatus> _users;
        private static GameRoom[] _rooms;

        private static ServerCommandsHandler _serverCommandsHandler;
        
        public static void Main(string[] args) {
            _users = new Dictionary<UserIP, UserStatus>();
            CreateRooms(10, out _rooms);
            _serverCommandsHandler = new ServerCommandsHandler(_rooms, _users);
            
            Console.WriteLine("Server started");
            ServerListener srv = new ServerListener(50900, _serverCommandsHandler.HandleData);
            srv.StartListening();
        }
        
        private static void CreateRooms(int count, out GameRoom[] rooms) {
            rooms = new GameRoom[count];
            for (byte roomId = 0; roomId < rooms.Length; roomId++) rooms[roomId] = new GameRoom(RoomStatus.Empty);
        }
    }
}