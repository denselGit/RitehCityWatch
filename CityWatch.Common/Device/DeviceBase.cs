using CityWatch.Common.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CityWatch.Common.Device
{
    public abstract class DeviceBase : Data
    {
        public string Name
        {
            get { return GetAttributeValue<string>(nameof(Name)); }
            set { SetAttributeValue(nameof(Name), value);}
        }

        public string DeviceId
        {
            get { return GetAttributeValue(nameof(DeviceId)); }
            set { SetAttributeValue(nameof (DeviceId), value); }
        }

        public DeviceBase(string name, string deviceId)
        {
            Name = name;
            DeviceId = deviceId;    
        }

        public void RaiseDeviceEvent(EventBase e)
        {
            log.Info($"Device [{DeviceId}] {Name} is raising event {e}...");
        }
    }
}
