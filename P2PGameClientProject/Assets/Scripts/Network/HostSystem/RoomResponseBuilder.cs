using P2PGameClientProject.Network.Data;
using P2PGameClientProject.Network.Data.Messages.Commands;
using P2PGameClientProject.Network.Data.Messages.Responses;

namespace P2PGameClientProject.Network.HostSystem {
    public class RoomResponseBuilder {
        public RoomResponse GetHostAddress(ActionResult actionResult, UserIP hostAddress) => new RoomResponse(actionResult, hostAddress.GetBytes());

        public RoomResponse GetUsersAddresses(ActionResult actionResult, UserIP userAddress) => new RoomResponse(actionResult, userAddress.GetBytes());
         
        public RoomResponse Disconnect(ActionResult actionResult) => new RoomResponse(actionResult);
         
        public RoomResponse SetMeAtHost(ActionResult actionResult) => new RoomResponse(actionResult);

        public RoomResponse SetOtherAtHost(ActionResult actionResult, UserIP newHostAddress) => new RoomResponse(actionResult, newHostAddress.GetBytes());
        
        public RoomResponse CreateFromByteData(byte[] data) => new RoomResponse(data); 
    }
}