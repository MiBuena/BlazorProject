﻿using IdentityServer4.Extensions;
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

        [HttpGet("itemsnames/{searchWord}")]
        public IActionResult GetItemsNames(string searchWord)
        {
            var response = _itemsDataService.GetItemsNames(searchWord, this.UserId);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("overview/{dto.PageSize:int?}/{dto.SkipItems:int?}/{dto.OrderByColumn?}/{dto.OrderByDirection?}/{dto.SearchWord?}/{dto.SearchDate?}")]
        public IActionResult GetOverviewItems([FromQuery] FilterPatemetersDto dto)
        {
            var response = _itemsDataService.GetItemsOverviewPageModel(this.UserId, dto);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            var response = _itemsDataService.GetItem(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
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

            var response = _itemsDataService.AddItem(this.UserId, itemDto);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Created("items", response);
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

            var getItemResponse = _itemsDataService.GetItem(itemDto.Id);
            if (!getItemResponse.IsSuccess || getItemResponse.Data == null)
            {
                return BadRequest(getItemResponse);
            }

            var updateResponse = _itemsDataService.UpdateItem(this.UserId, itemDto);
            if(!updateResponse.IsSuccess)
            {
                return BadRequest(updateResponse);
            }

            return Ok(updateResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var getItemResponse = _itemsDataService.GetItem(id);
            if (!getItemResponse.IsSuccess || getItemResponse.Data == null)
            {
                return BadRequest(getItemResponse);
            }

            var deleteResponse = _itemsDataService.DeleteItem(id);

            if(!deleteResponse.IsSuccess)
            {
                return BadRequest(deleteResponse);
            }

            return Ok(deleteResponse);
        }
    }
}
