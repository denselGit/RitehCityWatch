using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    [Table("SubCategory")]
    public class SubCategory : Data
    {
        [Key]
        public int IdSubCategory { get; set; }
        public int IdCategory { get; set; }
        public string Name {  get; set; }
    }
}
