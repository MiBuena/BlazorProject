using ListGenerator.Models.Dtos;
using ListGenerator.Web.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsDataService _itemsDataService;


        public ItemsController(IItemsDataService itemsDataService)
        {
            _itemsDataService = itemsDataService;
        }

        [HttpGet("overview")]
        public IActionResult GetOverviewItems()
        {
            var dtos = _itemsDataService.GetOverviewItemsModels();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            var dto = _itemsDataService.GetItem(id);
            return Ok(dto);
        }

        [HttpGet("shoppinglist/{secondReplenishmentDate}")]
        public IActionResult GetShoppingList(string secondReplenishmentDate)
        {
            var shoppingItems = _itemsDataService.GetShoppingList(secondReplenishmentDate);
            return Ok(shoppingItems);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] ItemDto itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdItemId = _itemsDataService.AddItem(itemDto);

            return Created("items", createdItemId);
        }

        [HttpPut]
        public IActionResult UpdateItem([FromBody] ItemDto itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = _itemsDataService.GetItem(itemDto.Id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            _itemsDataService.UpdateItem(itemDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var itemToDelete = _itemsDataService.GetItem(id);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            _itemsDataService.DeleteItem(id);

            return Ok();
        }
    }
}
