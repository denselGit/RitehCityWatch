using CityWatch.Common;
using CityWatch.Server.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWatch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : BaseController
    {
        IServiceRepository ServiceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        [HttpGet(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Common.Service>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await ServiceRepository.GetAll();

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
        [ProducesResponseType(typeof(Common.Service), 200)]
        public async Task<IActionResult> Post([FromBody] Service Service)
        {
            var result = await ServiceRepository.Save(Service);
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
        [ProducesResponseType(typeof(Common.Service), 200)]
        public async Task<IActionResult> Patch([FromBody] Service Service)
        {
            var result = await ServiceRepository.Save(Service);
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
        [ProducesResponseType(typeof(Common.Service), 200)]
        public async Task<IActionResult> Delete([FromBody] Service Service)
        {
            var result = await ServiceRepository.Delete(Service);
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
