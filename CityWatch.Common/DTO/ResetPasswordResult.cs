using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common.DTO
{
    public class ResetPasswordResult
    {
        public EResetPasswordResult Result {  get; set; }
        public enum EResetPasswordResult
        {
            OK = 0,
            PasswordMismatch = -1,
            LinkExpired = -2,
            UnknownLink = -3,
            ServerError = -4
        }
    }
}
