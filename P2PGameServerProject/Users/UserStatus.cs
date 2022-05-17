namespace P2PGameServerProject.Users {
    public enum UserStatus : byte {
        Online = 0,
        Waiting = 1,
        InGame = 2,
        Sleep = 3,
        Offline = 4
    }
}