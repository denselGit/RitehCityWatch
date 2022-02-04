using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    public class TechnicalDevice : Data
    {
        [Key]
        public int IdTechnicalDevice { get; set; }
        public int IdTenant { get; set; }
        public string Name { get; set; }
        public string DeviceType { get; set; }
    }
}
