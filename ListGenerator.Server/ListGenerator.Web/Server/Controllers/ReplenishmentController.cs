﻿using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Web.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReplenishmentController : IdentityController
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
