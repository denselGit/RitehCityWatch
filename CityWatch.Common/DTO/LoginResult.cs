using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.DTO
{
    public class LoginResult
    {
        public User User {  get; set; }
        public string TempToken {  get; set; }
        public string Token { get; set;  }
        public ELoginResult Result {  get; set; }   
        
        public enum ELoginResult
        {
            TwoFactorRequired = 1,
            Success = 0,
            InvalidEmailOrPassword = -1,
            ServeError = -2,
            Disabled = -3
        }
    }
}
