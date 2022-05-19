using System;
using P2PGameClientProject.Network.Data.Messages.Commands;

namespace P2PGameClientProject.Network.ServerSystem {
    public class RoomRequestBuilder {
        public byte[] GetHostAddress(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new [] { (byte)ServerCommand.RoomSubCommand, (byte)RoomCommand.GetHostAddress, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3] };
        }
        
        public byte[] GetUsersAddresses(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new [] { (byte)ServerCommand.RoomSubCommand, (byte)RoomCommand.GetUsersAddresses, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3] };
        }
        
        public byte[] Disconnect(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new [] { (byte)ServerCommand.RoomSubCommand, (byte)RoomCommand.Disconnect, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3] };
        }
        
        public byte[] SetMeAtHost(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new [] { (byte)ServerCommand.RoomSubCommand, (byte)RoomCommand.SetMeAtHost, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3] };
        }
        
        public byte[] SetOtherAtHost(int roomId) {
            byte[] roomIdBytes = BitConverter.GetBytes(roomId);
            return new [] { (byte)ServerCommand.RoomSubCommand, (byte)RoomCommand.SetOtherAtHost, roomIdBytes[0], roomIdBytes[1], roomIdBytes[2], roomIdBytes[3] };
        }
    }
}