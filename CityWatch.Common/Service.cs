using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    [Table("Service")]
    public class Service : Data
    {
        [Key]
        public int IdService { get; set; }
        public int IdTenant { get; set; }
        public string Name {  get; set; }
        public string Description {  get; set; }
    }
}
