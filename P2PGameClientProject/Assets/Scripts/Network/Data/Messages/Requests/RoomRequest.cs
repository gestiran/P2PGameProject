using System;
using P2PGameClientProject.Extensions;
using P2PGameClientProject.Network.Data.Messages.Commands;

namespace P2PGameClientProject.Network.Data.Messages.Requests {
    public readonly struct RoomRequest {
        public RoomCommand command => _command;
        public DateTime dateTime => _dateTime;
        public byte[] message => _message;

        private readonly RoomCommand _command;
        private readonly DateTime _dateTime;
        private readonly byte[] _message;

        public RoomRequest(RoomCommand command, byte[] message) {
            _command = command;
            _dateTime = DateTime.Now;
            _message = message;
        }
        
        public RoomRequest(RoomCommand command) {
            _command = command;
            _dateTime = DateTime.Now;
            _message = new byte[0];
        }
        
        public RoomRequest(byte[] data) {
            _command = (RoomCommand)data[0];
            _dateTime = DateTime.FromBinary(BitConverter.ToInt64(data, sizeof(RoomCommand)));
            _message = data.GetRange(sizeof(RoomCommand) + sizeof(long));
        }

        public byte[] GetBytes() {
            byte[] result = new byte[sizeof(RoomCommand) + sizeof(long) + _message.Length];
            
            int resultId = 0;
            
            result[resultId++] = (byte) _command;
            byte[] dateTimeBytes = BitConverter.GetBytes(_dateTime.ToBinary());
            
            for (int dateTimeId = 0; dateTimeId < dateTimeBytes.Length; resultId++, dateTimeId++) result[resultId] = dateTimeBytes[dateTimeId];
            for (int messageId = 0; messageId < _message.Length; resultId++, messageId++) result[resultId] = _message[messageId];

            return result;
        }
    }
}