using P2PGameServerProject.Data;
using P2PGameServerProject.Data.Messages.Commands;

namespace P2PGameServerProject.Messages.RoomMessages {
    public class RoomResponseBuilder {
        public byte[] GetHostAddress(ActionResult result, UserIP hostIp) {
            byte[] hostIpBytes = hostIp.GetBytes();
            return new [] { (byte)result, (byte)RoomCommand.GetHostAddress, hostIpBytes[0], hostIpBytes[1], hostIpBytes[2], hostIpBytes[3] };
        }
        
        public byte[] GetUsersAddresses(ActionResult result, UserIP userIp) {
            byte[] hostIpBytes = userIp.GetBytes();
            return new [] { (byte)result, (byte)RoomCommand.GetUsersAddresses, hostIpBytes[0], hostIpBytes[1], hostIpBytes[2], hostIpBytes[3] };
        }
        
        public byte[] Disconnect(ActionResult result) => new [] { (byte)result, (byte)RoomCommand.Disconnect };
        
        public byte[] SetMeAtHost(ActionResult result) => new [] { (byte)result, (byte)RoomCommand.SetMeAtHost };
        
        public byte[] SetOtherAtHost(ActionResult result, UserIP newHostIp) {
            byte[] hostIpBytes = newHostIp.GetBytes();
            return new [] { (byte)result, (byte)RoomCommand.SetOtherAtHost, hostIpBytes[0], hostIpBytes[1], hostIpBytes[2], hostIpBytes[3] };
        }
    }
}