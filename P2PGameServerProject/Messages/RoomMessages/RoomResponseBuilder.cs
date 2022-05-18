using P2PGameServerProject.Users;

namespace P2PGameServerProject.Messages.RoomMessages {
    public class RoomResponseBuilder {
        public byte[] GetHostAddress(ActionResult result, UserIPKey hostIp) {
            byte[] hostIpBytes = hostIp.GetBytes();
            return new [] { (byte)result, (byte)RoomCommand.GetHostAddress, hostIpBytes[0], hostIpBytes[1], hostIpBytes[2], hostIpBytes[3] };
        }
        
        public byte[] GetUsersAddresses(ActionResult result, UserIPKey userIp) {
            byte[] hostIpBytes = userIp.GetBytes();
            return new [] { (byte)result, (byte)RoomCommand.GetUsersAddresses, hostIpBytes[0], hostIpBytes[1], hostIpBytes[2], hostIpBytes[3] };
        }
        
        public byte[] Disconnect(ActionResult result) => new [] { (byte)result, (byte)RoomCommand.Disconnect };
        
        public byte[] SetMeAtHost(ActionResult result) => new [] { (byte)result, (byte)RoomCommand.SetMeAtHost };
        
        public byte[] SetOtherAtHost(ActionResult result, UserIPKey newHostIp) {
            byte[] hostIpBytes = newHostIp.GetBytes();
            return new [] { (byte)result, (byte)RoomCommand.SetOtherAtHost, hostIpBytes[0], hostIpBytes[1], hostIpBytes[2], hostIpBytes[3] };
        }
    }
}