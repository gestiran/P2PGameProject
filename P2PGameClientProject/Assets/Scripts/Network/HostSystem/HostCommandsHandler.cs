using System.Net;
using P2PGameClientProject.Network.ClientSystem;
using P2PGameClientProject.Network.Data.Messages.Commands;
using P2PGameClientProject.Network.Data.Messages.Requests;
using P2PGameClientProject.Network.Data.Messages.Responses;

namespace P2PGameClientProject.Network.HostSystem {
    public class HostCommandsHandler {
        private readonly RoomRequestBuilder _requestBuilder;
        private readonly RoomResponseBuilder _responseBuilder;

        public HostCommandsHandler() {
            _requestBuilder = new RoomRequestBuilder();
            _responseBuilder = new RoomResponseBuilder();
        }
        
        public bool HandleData(IPAddress address, byte[] data, out byte[] result) {
            RoomRequest request = _requestBuilder.CreateFromByteData(data);
            RoomResponse response;
            
            switch (request.command) {
                case RoomCommand.GetHostAddress: break;
                case RoomCommand.GetUsersAddresses: break;
                case RoomCommand.Disconnect: break;
                case RoomCommand.SetMeAtHost: break;
                case RoomCommand.SetOtherAtHost: break;
            }

            result = new byte[0];
            return false;
        }
    }
}