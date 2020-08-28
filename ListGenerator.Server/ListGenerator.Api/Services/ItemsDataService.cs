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
        private readonly IReplenishmentDataService _replenishmentDataService;

        public ItemsDataService(IRepository<Item> itemsRepository, IMapper mapper, IDateTimeProvider dateTimeProvider, IReplenishmentDataService replenishmentDataService)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _replenishmentDataService = replenishmentDataService;
        }

        public IEnumerable<ItemOverviewDto> GetOverviewItemsModels()
        {
            var dtos = _itemsRepository.All()
                .Select(x => new ItemOverviewDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReplenishmentPeriod = x.ReplenishmentPeriod,
                    NextReplenishmentDate = x.NextReplenishmentDate,
                    LastReplenishmentDate = x.Purchases
                                                    .OrderByDescending(y => y.Replenishment.Date)
                                                    .Select(m => m.Replenishment.Date)
                                                    .FirstOrDefault(),
                    LastReplenishmentQuantity = x.Purchases
                                                    .OrderByDescending(y => y.Replenishment.Date)
                                                    .Select(m => m.Quantity)
                                                    .FirstOrDefault(),
                })
                .ToList();

            return dtos;
        }
        public ItemDto GetItem(int itemId)
        {
            var query = _itemsRepository.All().Where(x => x.Id == itemId);

            var dto = _mapper.ProjectTo<ItemDto>(query).FirstOrDefault();

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
            var itemToUpdate = _itemsRepository.All().FirstOrDefault(x => x.Id == itemDto.Id);

            if(itemToUpdate != null)
            {
                var oldReplenishmentPeriod = itemToUpdate.ReplenishmentPeriod;
                var newReplenishmentPeriod = itemDto.ReplenishmentPeriod;

                if (newReplenishmentPeriod != oldReplenishmentPeriod)
                {
                    var newItemNextReplenishmentDate = _replenishmentDataService.RegenerateNextPurchaseDateTime(itemToUpdate.Id, newReplenishmentPeriod);
                    itemToUpdate.NextReplenishmentDate = newItemNextReplenishmentDate;
                }

                itemToUpdate.Name = itemDto.Name;
                itemToUpdate.ReplenishmentPeriod = itemDto.ReplenishmentPeriod;
               
                _itemsRepository.Update(itemToUpdate);
                _itemsRepository.SaveChanges();
            }
        }

        public void DeleteItem(int id)
        {
            var itemToDelete = _itemsRepository.All().FirstOrDefault(x => x.Id == id);

            if (itemToDelete != null)
            {
                _itemsRepository.Delete(itemToDelete);
                _itemsRepository.SaveChanges();
            }
        }

        public IEnumerable<ItemDto> GetShoppingList()
        {
            var dateTimeNow = _dateTimeProvider.GetDateTimeNow();

            var query = _itemsRepository.All()
                .Where(x => x.NextReplenishmentDate.Date <= dateTimeNow.Date);

            var itemsNeedingReplenishment = _mapper.ProjectTo<ItemDto>(query).ToList();
            
            return itemsNeedingReplenishment;
        }
    }
}
