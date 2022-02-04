using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWatch.Common
{
    [Table("User")]
    public class User : Data
    {
        [Key]
        public int IdUser { get; set; }
        public int IdTenant { get; set; }
        public EUserStatus Status { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? IdCity { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string TwoFactorEnabled { get; set; }
        public DateTime? PasswordResetDateTime { get; set; }
        public string PasswordResetLink { get; set; }
        public string ConfirmLink { get; set; }
        public string Note { get; set; }

        public enum EUserStatus
        {
            OK = 0,
            ConfirmationRequired = -1,
            Disabled = -2
        }

        public User WithoutPassword()
        {
            return new User()
            {
                IdUser = this.IdUser,
                IdTenant = this.IdTenant,
                Status = this.Status,
                Name = this.Name,
                Address = this.Address,
                Email = this.Email,
                Mobile = this.Mobile,
                Role = this.Role,
                Password = "",
                TwoFactorEnabled = this.TwoFactorEnabled,
                ConfirmLink = this.ConfirmLink,
                Note = this.Note,
                IdCity = this.IdCity,
                PasswordResetDateTime = this.PasswordResetDateTime,
                PasswordResetLink = this.PasswordResetLink
            };
        }
    }
}
