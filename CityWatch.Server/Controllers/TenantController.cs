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
    public class TenantController : BaseController
    {
        ITenantRepository TenantRepository;
        public TenantController(ITenantRepository tenantRepository)
        {
            TenantRepository = tenantRepository;
        }


        [HttpGet(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Common.Tenant>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await TenantRepository.GetAll();

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
        [ProducesResponseType(typeof(Common.Tenant), 200)]
        [Authorize(Roles = "sysop")]
        public async Task<IActionResult> Post([FromBody] Tenant Tenant)
        {
            var result = await TenantRepository.Save(Tenant);
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
        [ProducesResponseType(typeof(Common.Tenant), 200)]
        [Authorize(Roles = "sysop")]
        public async Task<IActionResult> Patch([FromBody] Tenant Tenant)
        {
            var result = await TenantRepository.Save(Tenant);
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
        [ProducesResponseType(typeof(Common.Tenant), 200)]
        [Authorize(Roles = "sysop")]
        public async Task<IActionResult> Delete([FromBody] Tenant Tenant)
        {
            var result = await TenantRepository.Delete(Tenant);
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
