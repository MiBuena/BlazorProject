using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Api.Interfaces;
using ListGenerator.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplenishmentController : ControllerBase
    {
        private readonly IReplenishmentDataService _replenishmentDataService;

        public ReplenishmentController(IReplenishmentDataService replenishmentDataService)
        {
            _replenishmentDataService = replenishmentDataService;
        }

        [HttpPost("replenish")]
        public IActionResult ReplenishItems([FromBody] ReplenishmentDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _replenishmentDataService.ReplenishItems(model);

            return Ok();
        }
    }
}
