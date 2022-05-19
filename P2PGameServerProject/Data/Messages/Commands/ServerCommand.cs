namespace P2PGameServerProject.Data.Messages.Commands {
    public enum ServerCommand : byte {
        AddMeToConnected = 0,
        RemoveMeFromConnected = 1,
        SetStatus = 2,
        GetFreeRoom = 3,
        CreateRoom = 4,
        RemoveRoom = 5,
        SendMatchResult = 6,
        RoomSubCommand = 7
    }
}