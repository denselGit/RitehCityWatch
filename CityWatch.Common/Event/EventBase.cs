using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.Event
{
    public abstract class EventBase : Data
    {

        public int IdEvent { get; set; }
        public int IdDevice { get; set;  }
        public string Source { get; set; }
        public string Description {  get; set; }
        public DateTime EventDateTimeUTC { get; set; }

        public EventBase()
        {

        }

        public enum EEventType
        {
            Unknown = 0,
            Alarm,
            StatusChange,
        }
    }
}
