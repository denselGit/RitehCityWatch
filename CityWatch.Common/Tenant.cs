using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    [Table("Tenant")]
    public class Tenant : Data
    {
        [Key]
        public int IdTenant { get; set; }
        public int IsActive { get; set; }
        public string Name {  get; set; }
    }
}
