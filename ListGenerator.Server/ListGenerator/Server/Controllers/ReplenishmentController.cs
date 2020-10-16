using ListGenerator.Shared.Dtos;
using ListGenerator.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Server.Controllers
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


        [HttpGet("shoppinglist/{secondReplenishmentDate}")]
        public IActionResult GetShoppingList(string secondReplenishmentDate)
        {
            var shoppingItems = _replenishmentDataService.GetShoppingList(secondReplenishmentDate, this.UserId);
            return Ok(shoppingItems);
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
