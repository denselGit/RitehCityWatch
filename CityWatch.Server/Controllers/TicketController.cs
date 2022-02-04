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
    public class TicketController : BaseController
    {
        ITicketRepository TicketRepository;
        public TicketController(ITicketRepository ticketRepository)
        {
            TicketRepository = ticketRepository;
        }


        [HttpGet(Name = "[Controller][Action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Common.Ticket>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await TicketRepository.GetAll();

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
        [ProducesResponseType(typeof(Common.Ticket), 200)]
        [Authorize(Roles = "sysop")]
        public async Task<IActionResult> Post([FromBody] Ticket Ticket)
        {
            var result = await TicketRepository.Save(Ticket);
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
        [ProducesResponseType(typeof(Common.Ticket), 200)]
        [Authorize(Roles = "sysop")]
        public async Task<IActionResult> Patch([FromBody] Ticket Ticket)
        {
            var result = await TicketRepository.Save(Ticket);
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
        [ProducesResponseType(typeof(Common.Ticket), 200)]
        [Authorize(Roles = "sysop")]
        public async Task<IActionResult> Delete([FromBody] Ticket Ticket)
        {
            var result = await TicketRepository.Delete(Ticket);
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
