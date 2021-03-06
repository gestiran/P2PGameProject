using System;
using P2PGameClientProject.Extensions;
using P2PGameClientProject.Network.Data.Messages.Commands;

namespace P2PGameClientProject.Network.Data.Messages.Responses {
    public readonly struct RoomResponse {
        public ActionResult command => _command;
        public DateTime dateTime => _dateTime;
        public byte[] message => _message;

        private readonly ActionResult _command;
        private readonly DateTime _dateTime;
        private readonly byte[] _message;

        public RoomResponse(ActionResult command, byte[] message) {
            _command = command;
            _dateTime = DateTime.Now;
            _message = message;
        }
        
        public RoomResponse(ActionResult command) {
            _command = command;
            _dateTime = DateTime.Now;
            _message = new byte[0];
        }
        
        public RoomResponse(byte[] data) {
            _command = (ActionResult)data[0];
            _dateTime = DateTime.FromBinary(BitConverter.ToInt64(data, sizeof(ActionResult)));
            _message = data.GetRange(sizeof(ActionResult) + sizeof(long));
        }

        public byte[] GetBytes() {
            byte[] result = new byte[sizeof(ActionResult) + sizeof(long) + _message.Length];
            
            int resultId = 0;
            
            result[resultId++] = (byte) _command;
            byte[] dateTimeBytes = BitConverter.GetBytes(_dateTime.ToBinary());
            
            for (int dateTimeId = 0; dateTimeId < dateTimeBytes.Length; resultId++, dateTimeId++) result[resultId] = dateTimeBytes[dateTimeId];
            for (int messageId = 0; messageId < _message.Length; resultId++, messageId++) result[resultId] = _message[messageId];

            return result;
        }
    }
}