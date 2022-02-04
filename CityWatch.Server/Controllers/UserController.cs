using CityWatch.Common;
using CityWatch.Server.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserRepository UserRepository;
        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Common.User>), 200)]
        [Authorize(Roles = "users.get")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<User> result = null;

            if (User.IsInRole("sysop"))
            {
                result = await UserRepository.GetAll();
            }
            else
            {
                result = await UserRepository.GetAll(GetTenantId());
            }

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Common.User), 200)]
        [Authorize(Roles = "users.save")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await UserRepository.Save(user);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPatch(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Common.User), 200)]
        [Authorize(Roles = "users.save")]
        public async Task<IActionResult> Patch([FromBody] User user)
        {
            var result = await UserRepository.Save(user);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Common.User), 200)]
        [Authorize(Roles = "users.save")]
        public async Task<IActionResult> Delete([FromBody] User user)
        {
            var result = await UserRepository.Delete(user);
            if (result)
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
