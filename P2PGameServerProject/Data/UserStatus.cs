namespace P2PGameServerProject.Data {
    public enum UserStatus : byte {
        Online = 0,
        Waiting = 1,
        InGame = 2,
        Sleep = 3,
        Offline = 4
    }
}