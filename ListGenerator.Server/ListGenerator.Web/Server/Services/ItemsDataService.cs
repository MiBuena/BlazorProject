using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ListGenerator.Data.Interfaces;
using ListGenerator.Data.Entities;
using ListGenerator.Web.Shared.Interfaces;

namespace ListGenerator.Web.Server.Services
{
    public class ItemsDataService : IItemsDataService
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IMapper _mapper;

        public ItemsDataService(IRepository<Item> itemsRepository, IMapper mapper)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
        }

        public IEnumerable<ItemOverviewDto> GetOverviewItemsModels(string userId)
        {
            var dtos = _itemsRepository.All()
                .Where(x=>x.UserId == userId)
                .Select(x => new ItemOverviewDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ReplenishmentPeriod = x.ReplenishmentPeriod,
                    NextReplenishmentDate = x.NextReplenishmentDate,
                    LastReplenishmentDate = x.Purchases
                                                    .OrderByDescending(y => y.ReplenishmentDate)
                                                    .Select(m => m.ReplenishmentDate)
                                                    .FirstOrDefault(),
                    LastReplenishmentQuantity = x.Purchases
                                                    .OrderByDescending(y => y.ReplenishmentDate)
                                                    .Select(m => m.Quantity)
                                                    .FirstOrDefault(),
                })
                .OrderBy(x => x.NextReplenishmentDate)
                .ToList();

            return dtos;
        }
        public ItemDto GetItem(int itemId)
        {
            var query = _itemsRepository.All().Where(x => x.Id == itemId);

            var dto = _mapper.ProjectTo<ItemDto>(query).FirstOrDefault();

            return dto;
        }

        public int AddItem(string userId, ItemDto itemDto)
        {
            var itemEntity = _mapper.Map<ItemDto, Item>(itemDto);

            itemEntity.UserId = userId;

            _itemsRepository.Add(itemEntity);

            _itemsRepository.SaveChanges();

            return itemEntity.Id;
        }

        public void UpdateItem(string userId, ItemDto itemDto)
        {
            var itemToUpdate = _itemsRepository.All().FirstOrDefault(x => x.Id == itemDto.Id);

            if(itemToUpdate != null)
            {
                itemToUpdate.Name = itemDto.Name;
                itemToUpdate.ReplenishmentPeriod = itemDto.ReplenishmentPeriod;
                itemToUpdate.NextReplenishmentDate = itemDto.NextReplenishmentDate;
                itemToUpdate.UserId = userId;
               
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

        public IEnumerable<ItemDto> GetShoppingList(string secondReplenishmentDate, string userId)
        {
            var date = DateTime.ParseExact(secondReplenishmentDate, "dd-MM-yyyy", null);

            var query = _itemsRepository.All()
                .Where(x => x.NextReplenishmentDate.Date < date
                && x.UserId == userId)
                .OrderBy(x => x.NextReplenishmentDate);

            var itemsNeedingReplenishment = _mapper.ProjectTo<ItemDto>(query).ToList();
            
            return itemsNeedingReplenishment;
        }
    }
}
