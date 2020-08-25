using ListGenerator.Api.Interfaces;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ListGenerator.Api.Services
{
    public class ItemsDataService : IItemsDataService
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemsDataService(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public IEnumerable<ShoppingListItem> CalculateGenerationList()
        {
            var a = _itemsRepository.All()
                .Where(x => DateTime.Compare(x.NextReplenishmentDate, DateTime.Now) < 1)
                .Select(x => new ShoppingListItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

            return a;
        }
    }


    public class ShoppingListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
