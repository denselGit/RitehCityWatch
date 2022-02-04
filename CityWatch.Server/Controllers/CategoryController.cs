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
    public class CategoryController : BaseController
    {
        ICategoryRepository CategoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }


        [HttpGet(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Common.Category>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await CategoryRepository.GetAll(GetTenantId());

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
        [ProducesResponseType(typeof(Common.Category), 200)]
        public async Task<IActionResult> Post([FromBody] Category Category)
        {
            var result = await CategoryRepository.Save(Category);
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
        [ProducesResponseType(typeof(Common.Category), 200)]
        public async Task<IActionResult> Patch([FromBody] Category Category)
        {
            var result = await CategoryRepository.Save(Category);
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
        [ProducesResponseType(typeof(Common.Category), 200)]
        public async Task<IActionResult> Delete([FromBody] Category Category)
        {
            var result = await CategoryRepository.Delete(Category);
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
