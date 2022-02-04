using CityWatch.Common.DTO;
using CityWatch.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CityWatch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IAuthService AuthService;
        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        /// <summary>
        /// Authenticates user using email and password
        /// </summary>
        /// <param name="loginData">Login data containing codename, email and password</param>
        /// <returns>Returns <see cref="LoginResult"/>LoginResult object with auth result data</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<LoginResult>> Authenticate([FromBody] Login loginData)
        {
            LoginResult result = await AuthService.Authenticate(loginData.Email, loginData.Password);

            if (result == null)
            {
                return StatusCode(500);
            }

            return result;
        }

        /// <summary>
        /// Request user password reset
        /// </summary>
        /// <param name="email">Email to reset password</param>
        /// <returns>Result of operation</returns>
        [AllowAnonymous]
        [HttpPost("RequestPasswordReset")]
        public async Task<ActionResult<bool>> RequestPasswordReset([FromBody] string email)
        {
            return Ok(await AuthService.RequestPasswordReset(email));
        }

        /// <summary>
        /// Resets user password
        /// </summary>
        /// <param name="reset">New password and reset link</param>
        /// <returns>Result of operation</returns>
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ResetPasswordResult>> ResetPassword([FromBody] ResetPassword reset)
        {
            var result = await AuthService.ResetPassword(reset.ResetLink, reset.Password, reset.PasswordRepeat);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
