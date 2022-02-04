using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.Device
{
    public class PayloadMessage
    {
        public string Topic {  get; set; }
        public byte[] Payload {  get; set; }
    }
}
