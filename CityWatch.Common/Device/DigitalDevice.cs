using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.Device
{
    public class DigitalDevice : DeviceBase
    {
        public bool State
        {
            get { return GetAttributeValue<bool>(nameof(State)); }
            set { SetAttributeValue(nameof(State), value.ToString()); }
        }

        public DigitalDevice(string deviceId, string name) : base(deviceId, name)
        {
            DeviceId = deviceId;
            Name = name;
        }

        public void ToggleState(bool newState)
        {
            if (newState != State)
            {
                State = newState;
            }
        }
    }
}
