using System;
using P2PGameClientProject.Extensions;
using P2PGameClientProject.Network.Data.Messages.Commands;

namespace P2PGameClientProject.Network.Data.Messages.Requests {
    public readonly struct ServerRequest {
        public ServerCommand command => _command;
        public DateTime dateTime => _dateTime;
        public byte[] message => _message;

        private readonly ServerCommand _command;
        private readonly DateTime _dateTime;
        private readonly byte[] _message;

        public ServerRequest(ServerCommand command, byte[] message) {
            _command = command;
            _dateTime = DateTime.Now;
            _message = message;
        }
        
        public ServerRequest(byte[] data) {
            _command = (ServerCommand)data[0];
            _dateTime = DateTime.FromBinary(BitConverter.ToInt64(data, sizeof(ServerCommand)));
            _message = data.GetRange(sizeof(ServerCommand) + sizeof(long));
        }

        public byte[] GetBytes() {
            byte[] result = new byte[sizeof(ServerCommand) + sizeof(long) + _message.Length];
            
            int resultId = 0;
            
            result[resultId++] = (byte) _command;
            byte[] dateTimeBytes = BitConverter.GetBytes(_dateTime.ToBinary());
            
            for (int dateTimeId = 0; dateTimeId < dateTimeBytes.Length; resultId++, dateTimeId++) result[resultId] = dateTimeBytes[dateTimeId];
            for (int messageId = 0; messageId < _message.Length; resultId++, messageId++) result[resultId] = _message[messageId];

            return result;
        }
    }
}