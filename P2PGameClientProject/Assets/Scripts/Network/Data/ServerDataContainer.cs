using UnityEngine;

namespace P2PGameClientProject.Network.Data {
    [CreateAssetMenu(fileName = "ServerDataContainer", menuName = "Network/ServerDataContainer", order = 0)]
    public class ServerDataContainer : ScriptableObject {
        public string address => _address;
        public int port => _port;

        [SerializeField] private string _address = "127.0.0.1";
        [SerializeField] private int _port = 50900;
    }
}