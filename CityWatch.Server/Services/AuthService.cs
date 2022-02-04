using CityWatch.Common;
using CityWatch.Common.DTO;
using CityWatch.Library;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityWatch.Server.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Authenticate(string email, string password);
        Task<LoginResult> AuthenticateStepTwo(string pin, string token);
        Task<bool> RequestPasswordReset(string email);
        Task<ResetPasswordResult> ResetPassword(string link, string password, string passwordRepeat);
    }
    public class AuthService : IAuthService
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IConfiguration Configuration;
        private IEmailService EmailService;
        private IDatabaseService DatabaseService;

        private string TokenSecurityKey = "";
        private string TokenIssuer = "";
        private string TokenAudience = "";

        public AuthService(IConfiguration configuration, IEmailService emailService, IDatabaseService databaseService)
        {
            Configuration = configuration;
            EmailService = emailService;
            DatabaseService = databaseService;

            TokenSecurityKey = Configuration.GetValue<string>("TokenSecurityKey");
            TokenIssuer = Configuration.GetValue<string>("TokenIssuer");
            TokenAudience = Configuration.GetValue<string>("TokenAudience");
        }

        public async Task<LoginResult> Authenticate(string email, string password)
        {
            log.Info($"Authenticating user { email }");

            User user = null;

            try
            {
                string pass = Encryption.ComputeSha256Hash($"{email}:{password}");
                using (var ctx = DatabaseService.GetContext())
                {
                    user = await ctx.QueryFirstOrDefaultAsync<User>("SELECT * from [User] where Email=@email AND Password=@pass", new { email, pass });
                }

                if (user == null)
                {
                    return new LoginResult() { Result = LoginResult.ELoginResult.InvalidEmailOrPassword };
                }

                if (user.Status == User.EUserStatus.Disabled)
                {
                    return new LoginResult() { Result = LoginResult.ELoginResult.Disabled };
                }

                if (string.IsNullOrEmpty(user.TwoFactorEnabled))
                {
                    return new LoginResult() { User = user.WithoutPassword(), Result = LoginResult.ELoginResult.TwoFactorRequired };
                }

                return new LoginResult()
                {
                    Result = LoginResult.ELoginResult.Success,
                    Token = GenerateJwt(user, "db", "password"),
                    User = user.WithoutPassword()
                };
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error authenticating user");
                return new LoginResult() { Result = LoginResult.ELoginResult.ServeError };
            }
        }

        public Task<LoginResult> AuthenticateStepTwo(string pin, string token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RequestPasswordReset(string email)
        {
            log.Info($"User {email} has requested password reset");

            try
            {
                User user = null;

                if (string.IsNullOrEmpty(email))
                {
                    return false;
                }

                using (var ctx = DatabaseService.GetContext())
                {
                    user = await ctx.QueryFirstOrDefaultAsync<User>("SELECT * FROM [User] where Email=@email", new { email });
                    if (user != null)
                    {
                        user.PasswordResetDateTime = DateTime.Now;
                        user.PasswordResetLink = Guid.NewGuid().ToString();
                        if(await ctx.UpdateAsync(user))
                        {
                            return EmailService.SendEmail(user.Email, "CityWatch - Reset lozinke", "", true);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error resetting password for " + email);
                return false;
            }
        }

        public async Task<ResetPasswordResult> ResetPassword(string link, string password, string passwordRepeat)
        {
            try
            {
                User user = null;
                using (var ctx = DatabaseService.GetContext())
                {
                    if (password != passwordRepeat) return new ResetPasswordResult() { Result = ResetPasswordResult.EResetPasswordResult.PasswordMismatch };

                    user = await ctx.QueryFirstOrDefaultAsync<User>("SELECT * FROM [User] WHERE PasswordResetLink=@link", new { link });
                    if (user != null)
                    {
                        if (user.PasswordResetDateTime.GetValueOrDefault().AddHours(8) < DateTime.Now) return new ResetPasswordResult() { Result = ResetPasswordResult.EResetPasswordResult.UnknownLink };
                        user.Password = Encryption.ComputeSha256Hash($"{user.Email}:{user.Password}");
                        if (await ctx.UpdateAsync(user))
                        {
                            return new ResetPasswordResult() { Result = ResetPasswordResult.EResetPasswordResult.OK };
                        }
                        else
                        {
                            return new ResetPasswordResult() { Result = ResetPasswordResult.EResetPasswordResult.ServerError };
                        }
                    }
                    else
                    {
                        return new ResetPasswordResult() { Result = ResetPasswordResult.EResetPasswordResult.UnknownLink };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error resetting password");
                return new ResetPasswordResult() { Result = ResetPasswordResult.EResetPasswordResult.ServerError };
            }
        }

        /// <summary>
        /// Generira JWT token iz login podataka i dodaje osnovne claimove
        /// </summary>
        /// <param name="user"><seealso cref="User"/> korisnik</param>
        /// <param name="authServer">Server koji je izvržio autorizaciju (baza, AD ili RADIUS)</param>
        /// <param name="authMethod">Način prijave u sustav - lozinka, smart card...</param>
        /// <returns></returns>
        private string GenerateJwt(User user, string authServer, string authMethod = "password")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.IdUser.ToString()),
                    new Claim("IdTenant", user.IdTenant.ToString()),
                    new Claim(ClaimTypes.AuthorizationDecision, authServer),
                    new Claim(ClaimTypes.AuthenticationMethod, authMethod),
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = TokenAudience,
                Issuer = TokenIssuer
            };
            foreach(var role in user.Role.Split(','))
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Trim()));
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
