using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using ListGenerator.Api.Interfaces;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Api.Controllers
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

        [HttpGet("shoppinglist")]
        public IActionResult GetShoppingList()
        {
            var shoppingItems = _itemsDataService.GetShoppingList();
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
