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
    public class SubCategoryController : BaseController
    {
        ISubCategoryRepository SubCategoryRepository;
        public SubCategoryController(ISubCategoryRepository subCategoryRepository)
        {
            SubCategoryRepository = subCategoryRepository;
        }


        [HttpGet(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Common.SubCategory>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] int idCategory)
        {
            var result = await SubCategoryRepository.GetAll(idCategory);

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
        [ProducesResponseType(typeof(Common.SubCategory), 200)]
        public async Task<IActionResult> Post([FromBody] SubCategory SubCategory)
        {
            var result = await SubCategoryRepository.Save(SubCategory);
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
        [ProducesResponseType(typeof(Common.SubCategory), 200)]
        public async Task<IActionResult> Patch([FromBody] SubCategory SubCategory)
        {
            var result = await SubCategoryRepository.Save(SubCategory);
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
        [ProducesResponseType(typeof(Common.SubCategory), 200)]
        public async Task<IActionResult> Delete([FromBody] SubCategory SubCategory)
        {
            var result = await SubCategoryRepository.Delete(SubCategory);
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
