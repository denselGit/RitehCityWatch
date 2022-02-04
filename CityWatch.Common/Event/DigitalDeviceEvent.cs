using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.Event
{
    public class DigitalDeviceEvent : TechnicalEvent
    {
        public bool State { get; set; }

        public DigitalDeviceEvent()
        {

        }
    }
}
