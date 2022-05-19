using P2PGameClientProject.Network.Data.Messages.Commands;
using P2PGameClientProject.Network.Data.Messages.Requests;

namespace P2PGameClientProject.Network.ClientSystem {
    public class RoomRequestBuilder {
        public RoomRequest GetHostAddress() => new RoomRequest(RoomCommand.GetHostAddress);

        public RoomRequest GetUsersAddresses() => new RoomRequest(RoomCommand.GetUsersAddresses);
         
        public RoomRequest Disconnect() => new RoomRequest(RoomCommand.Disconnect);
         
        public RoomRequest SetMeAtHost() => new RoomRequest(RoomCommand.SetMeAtHost);
        
        public RoomRequest SetOtherAtHost() => new RoomRequest(RoomCommand.SetOtherAtHost);
        
        public RoomRequest CreateFromByteData(byte[] data) => new RoomRequest(data);
    }
}