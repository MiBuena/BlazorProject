using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ListGenerator.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchasesRepository _purchasesRepository;

        private readonly IMapper _mapper;

        public PurchaseController(IPurchasesRepository purchasesRepository, IMapper mapper)
        {
            _purchasesRepository = purchasesRepository;
            _mapper = mapper;
        }

        [HttpGet("itemslastpurchase")]
        public IActionResult GetItemsLastPurchase()
        {
            return Ok(_purchasesRepository.GetItemsLastPurchases());
        }
    }
}
