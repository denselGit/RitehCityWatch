using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    [Table("Category")]
    public class Category : Data
    {
        [Key]
        public int IdCategory { get; set; }
        public int IdTenant { get; set;  }
        public string Name {  get; set; }
    }
}
