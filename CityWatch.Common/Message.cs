using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    public class Message
    {
        public string DeviceId { get; set; }
        public string RawData { get; set; }
        public EMessageType MessageType { get; set; }
        public EDeviceType DeviceType { get; set; }

        public DateTime Timestamp { get; set; }
        public bool State { get; set; }
        public decimal Value { get; set; }

        public Message(EMessageType messageType, string deviceId, string rawData)
        {
            MessageType = messageType;
            DeviceId = deviceId;
            RawData = rawData;
        }

        public enum EMessageType
        {
            Unknown = 0,
            System  = 1,
            Device = 2
        }

        public enum EDeviceType
        {
            Unknown = 0,
            DigitalDevice = 1,
            AnalogDevice = 2
        }
    }
}
