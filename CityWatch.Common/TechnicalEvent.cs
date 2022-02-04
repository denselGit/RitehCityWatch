using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    public class TechnicalEvent : Data
    {
        public int IdTechnicalEvent { get; set; }
        public int IdTechnicalDevice { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTimeUTC { get; set; }
    }
}
