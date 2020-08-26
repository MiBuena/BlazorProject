using AutoMapper;
using ListGenerator.Api.Interfaces;
using ListGenerator.Common.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ItemsDataService(IRepository<Item> itemsRepository, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<ItemDto> GetOverviewItemsModels()
        {
            var dtos = _itemsRepository.All()
                .Select(x => new ItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReplenishmentPeriod = x.ReplenishmentPeriod,
                    NextReplenishmentDate = x.NextReplenishmentDate
                })
                .ToList();

            return dtos;
        }
        public ItemDto GetItem(int itemId)
        {
            var dto = _itemsRepository.All()
                .Select(x => new ItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReplenishmentPeriod = x.ReplenishmentPeriod,
                    NextReplenishmentDate = x.NextReplenishmentDate
                })
                .FirstOrDefault(x => x.Id == itemId);

            return dto;
        }

        public int AddItem(ItemDto itemDto)
        {
            var itemEntity = _mapper.Map<ItemDto, Item>(itemDto);

            itemEntity.NextReplenishmentDate = _dateTimeProvider.GetDateTimeNow();

            _itemsRepository.Add(itemEntity);

            _itemsRepository.SaveChanges();

            return itemEntity.Id;
        }

        public void UpdateItem(ItemDto itemDto)
        {
            var itemEntity = _mapper.Map<ItemDto, Item>(itemDto); 
            
            _itemsRepository.Update(itemEntity);

            _itemsRepository.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var itemToDelete = _itemsRepository.All().FirstOrDefault(x => x.Id == id);
            _itemsRepository.Delete(itemToDelete);
            _itemsRepository.SaveChanges();
        }

        public IEnumerable<ItemDto> GetShoppingList()
        {
            var dateTimeNow = _dateTimeProvider.GetDateTimeNow();

            var itemsNeedingReplenishment = _itemsRepository.All()
                .Where(x => x.NextReplenishmentDate <= dateTimeNow)
                .Select(x => new ItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReplenishmentPeriod = x.ReplenishmentPeriod,
                    NextReplenishmentDate = x.NextReplenishmentDate
                })
                .ToList();

            return itemsNeedingReplenishment;
        }

        public void ReplenishItems(ReplenishmentDto replenishmentData)
        {
            var allItems = _itemsRepository.All().ToList();

            foreach (var purchaseItem in replenishmentData.Purchaseitems)
            {
                var item = allItems.FirstOrDefault(x => x.Id == purchaseItem.ItemId);

                var coveredWeeks = double.Parse(purchaseItem.Quantity.ToString()) * item.ReplenishmentPeriod;

                var days = coveredWeeks * 7;

                var newReplenishmentDate = _dateTimeProvider.GetDateTimeNow().AddDays(days);

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
