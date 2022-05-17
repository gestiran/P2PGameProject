using System.Net;

namespace P2PGameServerProject.Users {
    public readonly struct UserIPKey : IEquatable<UserIPKey> {
        private readonly byte _ip_0;
        private readonly byte _ip_1;
        private readonly byte _ip_2;
        private readonly byte _ip_3;

        public UserIPKey(IPAddress address) {
            byte[] ipBytes = address.GetAddressBytes();
            _ip_0 = ipBytes[0];
            _ip_1 = ipBytes[1];
            _ip_2 = ipBytes[2];
            _ip_3 = ipBytes[3];
        } 

        public bool Equals(UserIPKey other) {
            if (_ip_0 != other._ip_0) return false;
            if (_ip_1 != other._ip_1) return false;
            if (_ip_2 != other._ip_2) return false;
            if (_ip_3 != other._ip_3) return false;
            return true;
        }
    }
}