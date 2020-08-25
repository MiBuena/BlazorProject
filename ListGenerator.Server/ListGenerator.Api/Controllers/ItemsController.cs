using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ListGenerator.Api.Interfaces;
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
        private readonly IItemRepository _itemRepository;

        private readonly IMapper _mapper;


        public ItemsController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] ItemViewModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemEntity = _mapper.Map<ItemViewModel, Item>(item);

            var createdItem = _itemRepository.AddItem(itemEntity);

            return Created("items", createdItem);
        }

        [HttpPut]
        public IActionResult UpdateItem([FromBody] ItemViewModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate =  _itemRepository.GetItemById(item.Id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            var itemEntity = _mapper.Map<ItemViewModel, Item>(item);

            _itemRepository.UpdateItem(itemEntity);

            return NoContent(); //success
        }


        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(_itemRepository.GetAllItems());
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            return Ok(_itemRepository.GetItemById(id));
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var itemToDelete = _itemRepository.GetItemById(id);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            _itemRepository.DeleteItem(id);

            return NoContent();//success
        }
    }
}
