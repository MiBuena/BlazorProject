using IdentityServer4.Extensions;
using ListGenerator.Shared.Dtos;
using ListGenerator.Server.Extensions;
using ListGenerator.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace ListGenerator.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : IdentityController
    {
        private readonly IItemsDataService _itemsDataService;


        public ItemsController(IItemsDataService itemsDataService)
        {
            _itemsDataService = itemsDataService;
        }

        [HttpGet("itemsnames/{searchWord?}")]
        public IActionResult GetItemsNames(string searchWord)
        {
            var response = _itemsDataService.GetItemsNames(searchWord, this.UserId);
            return Ok(response);
        }

        [HttpGet("overview/{dto.PageSize:int?}/{dto.SkipItems:int?}/{dto.OrderByColumn?}/{dto.OrderByDirection?}/{dto.SearchWord?}/{dto.SearchDate?}")]
        public IActionResult GetOverviewItems([FromQuery] FilterPatemetersDto dto)
        {
            var overviewDto = _itemsDataService.GetItemsOverviewPageModel(this.UserId, dto);
            return Ok(overviewDto);
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
            var shoppingItems = _itemsDataService.GetShoppingList(secondReplenishmentDate, this.UserId);
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

            var createdItemId = _itemsDataService.AddItem(this.UserId, itemDto);

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

            _itemsDataService.UpdateItem(this.UserId, itemDto);

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
