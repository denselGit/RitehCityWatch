using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.DTO
{
    public class ResetPassword
    {
        public string ResetLink { get; set; }
        public string Password {  get; set; }
        public string PasswordRepeat {  get; set; }
    }
}
