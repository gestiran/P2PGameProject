using System;
using System.Net;
#pragma warning disable 8614
#pragma warning disable 8610

namespace P2PGameServerProject.Data {
    public readonly struct UserIP : IEquatable<UserIP> {
        private readonly byte _ip_0;
        private readonly byte _ip_1;
        private readonly byte _ip_2;
        private readonly byte _ip_3;

        public UserIP(IPAddress address) {
            byte[] ipBytes = address.GetAddressBytes();
            _ip_0 = ipBytes[0];
            _ip_1 = ipBytes[1];
            _ip_2 = ipBytes[2];
            _ip_3 = ipBytes[3];
        }

        public byte[] GetBytes() => new [] {_ip_0, _ip_1, _ip_2, _ip_3};

        public bool Equals(UserIP other) {
            if (_ip_0 != other._ip_0) return false;
            if (_ip_1 != other._ip_1) return false;
            if (_ip_2 != other._ip_2) return false;
            if (_ip_3 != other._ip_3) return false;
            return true;
        }

        public override bool Equals(object obj) {
            if (obj != null && obj is UserIP userIpKey) return Equals(userIpKey);
            return false;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}