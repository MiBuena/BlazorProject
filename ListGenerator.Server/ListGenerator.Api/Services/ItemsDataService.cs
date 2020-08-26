using ListGenerator.Api.Interfaces;
using ListGenerator.Models;
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

        public IEnumerable<ItemOverviewDto> GetOverviewItemsModels()
        {
            var dtos = _itemsRepository.All()
                .Select(x => new ItemOverviewDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReplenishmentPeriod = x.ReplenishmentPeriod
                })
                .ToList();

            return dtos;
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

        public void ReplenishItems(IEnumerable<ReplenishmentData> replenishmentDatas)
        {
            var allItems = _itemsRepository.All().ToList();

            foreach (var replenishmentData in replenishmentDatas)
            {
                var item = allItems.FirstOrDefault(x => x.Id == replenishmentData.ItemId);

                var coveredWeeks = double.Parse(replenishmentData.Quantity) * item.ReplenishmentPeriod;

                var days = coveredWeeks * 7;

                var newReplenishmentDate = DateTime.Now.AddDays(days);

                item.NextReplenishmentDate = newReplenishmentDate;
            }

           _itemsRepository.SaveChanges();
        }
    }


    public class ShoppingListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
