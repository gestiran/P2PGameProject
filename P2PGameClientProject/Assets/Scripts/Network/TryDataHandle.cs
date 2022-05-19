using System.Net;

namespace P2PGameClientProject.Network {
    public delegate bool TryDataHandle(IPAddress address, byte[] data, out byte[] result);
}