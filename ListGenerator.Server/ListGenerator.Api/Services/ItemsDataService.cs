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
                    ReplenishmentPeriod = x.ReplenishmentPeriod
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
                    ReplenishmentPeriod = x.ReplenishmentPeriod
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
