namespace P2PGameClientProject.Network.Data.Messages.Commands {
    public enum RoomCommand : byte {
        GetHostAddress = 0,
        GetUsersAddresses = 1,
        Disconnect = 2,
        SetMeAtHost = 3,
        SetOtherAtHost = 4
    }
}