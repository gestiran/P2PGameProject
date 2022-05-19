namespace P2PGameClientProject.Network.HostSystem {
    public static class GameHost {
        private static readonly HostListener _listener;
        
        private const int _HOST_PORT = 50901;
        
        static GameHost() => _listener = new HostListener(_HOST_PORT, new HostCommandsHandler().HandleData);

        public static void StartHost() {
            if (_listener.isListening) return;
            _listener.StartListeningAsync();
        }

        public static void StopHost() {
            if (!_listener.isListening) return;
            _listener.StopListening();
        }
    }
}