using System.Net;

namespace P2PGameServerProject.Listeners {
    public delegate bool TryDataHandle(IPAddress address, byte[] data, out byte[] result);
}