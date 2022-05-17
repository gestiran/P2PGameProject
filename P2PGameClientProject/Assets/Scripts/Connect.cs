using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Connect : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _log;

    private string _serverIp;
    
    private int _serverPort = 50900;
    private int _hostPort = 50901;

    private enum Command : byte {
        SetHost,
        CheckHost,
        GetHostIP
    }

    private enum CommandResult : byte {
        Success,
        Failed
    }

    private void Awake() {
        _serverIp = "192.168.0.4";
    #if UNITY_EDITOR
        _serverIp = "127.0.0.1";
    #endif
    }

    public delegate bool TryDataHandle(IPAddress address, byte[] data, out byte[] result);
    
    private void Start() => CheckHost();

    private void CheckHost() => SendToServer(GetCommand(Command.CheckHost), ConvertResult);

    private void ConvertResult(byte[] result) {
        switch ((CommandResult) result[0]) {
            case CommandResult.Success:
                SendToServer(GetCommand(Command.GetHostIP), SendHelloToHost);
                break;

            case CommandResult.Failed:
                SendToServer(GetCommand(Command.SetHost), SetMeToHost);
                break;
        }
    }

    private void SendHelloToHost(byte[] result) {
        if ((CommandResult) result[0] == CommandResult.Failed) {
            _log.text += "Host is not find!\n";
            return;
        }
        
        byte[] address = new byte[result.Length - 1];
        for (byte i = 0; i < address.Length; i++) address[i] = result[i + 1];

        _log.text += "Host not parsed: ";
        for (int i = 0; i < address.Length; i++) _log.text += $"{address[i]}.";
        _log.text += "\n";

        IPAddress hostAddress = new IPAddress(address);

        _log.text += $"Hello send to {hostAddress}\n";
        SendData(hostAddress, _hostPort, Encoding.Unicode.GetBytes("Hello host!"), Empty);
    }

    private void Empty(byte[] result) {
        _log.text += "Is send\n";
    }

    private void SetMeToHost(byte[] result) {
        _log.text += "I am host\n";
        Task.Run(() => HostListener(HandleHostData));
    }

    private void HostListener(TryDataHandle dataHandle) {
        WaitForSecondsRealtime frame = new WaitForSecondsRealtime(1);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _hostPort);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(endPoint);
        socket.Listen(1);

        while (true) {
            Socket listener = socket.Accept();

            if (listener.RemoteEndPoint is IPEndPoint clientPoint) {
                List<byte> data = new List<byte>();
                byte[] buffer = new byte[256];
                int size;

                do {
                    size = listener.Receive(buffer);
                    for (int i = 0; i < size; i++) data.Add(buffer[i]);
                }
                while (listener.Available > 0);

                if (dataHandle(clientPoint.Address, data.ToArray(), out byte[] result)) listener.Send(result);
            }

            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }
    }

    private bool HandleHostData(IPAddress address, byte[] data, out byte[] result) {
        _log.text += $"HandleHostData: {Encoding.Unicode.GetString(data)}\n";
        result = new byte[] {0};
        return true;
    }

    private void SendToServer(byte[] data, ResultCallback Callback) => SendData(IPAddress.Parse(_serverIp), _serverPort, data, Callback);

    private void SendData(IPAddress address, int port, byte[] sendData, ResultCallback Callback) {
        IPEndPoint ipEndPoint = new IPEndPoint(address, port);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(ipEndPoint);
        socket.Send(sendData);
        
        List<byte> data = new List<byte>();
        byte[] buffer = new byte[256];
        int size;
        
        do {
            size = socket.Receive(buffer);
            for (int i = 0; i < size; i++) data.Add(buffer[i]);
        }
        while (socket.Available > 0);

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();

        _log.text += "Data: ";
        for (int i = 0; i < data.Count; i++) _log.text += $"{data[i]},";
        _log.text += "\n";

        Callback(data.ToArray());
    }

    private byte[] GetCommand(Command command) => new[] {
        (byte) command
    };

    private delegate void ResultCallback(byte[] result);
}