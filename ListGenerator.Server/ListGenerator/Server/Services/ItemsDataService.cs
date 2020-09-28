using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ListGenerator.Data.Interfaces;
using ListGenerator.Data.Entities;
using System.Linq.Expressions;

namespace ListGenerator.Server.Services
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

        public ItemsOverviewPageDto GetItemsOverviewPageModel(string userId, int? top, int? skip, string orderBy)
        {
            var dtos = GetOverviewItemsModels(userId, top, skip, orderBy);

            var itemsCount = _itemsRepository.All().Count();

            var pageDto = new ItemsOverviewPageDto()
            {
                OverviewItems = dtos,
                TotalItemsCount = itemsCount
            };

            return pageDto;
        }

        private IEnumerable<ItemOverviewDto> GetOverviewItemsModels(string userId, int? top, int? skip, string orderBy)
        {
            var query = GetBaseQuery(userId);

            string orderByColumn = null;
            string orderByDirection = null;

            if (orderBy != null)
            {
                orderByColumn = orderBy.Split(" ")[0];
                orderByDirection = orderBy.Split(" ")[1];
            }

            if (orderByColumn != null && orderByDirection != null)
            {
                query = Sort(orderByColumn, orderByDirection, query);
            }

            var pagedQuery = query
                .Skip(skip.Value)
                .Take(top.Value)
                .ToList();

            return pagedQuery;
        }

        private IQueryable<ItemOverviewDto> Sort(string orderByColumn, string orderByDirection, IQueryable<ItemOverviewDto> query)
        {
            if(orderByDirection == "asc")
            {
                query = SortByAscending(orderByColumn, query);
            }
            else
            {
                query = SortByDescending(orderByColumn, query);
            }

            return query;
        }

        private IQueryable<ItemOverviewDto> SortByAscending(string column, IQueryable<ItemOverviewDto> query)
        {
            switch (column)
            {
                case "Name":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "ReplenishmentPeriod":
                    query = query.OrderBy(x => x.ReplenishmentPeriod);
                    break;
                case "NextReplenishmentDate":
                    query = query.OrderBy(x => x.NextReplenishmentDate);
                    break;
                case "LastReplenishmentQuantity":
                    query = query.OrderBy(x => x.LastReplenishmentQuantity);
                    break;
                default:
                    break;
            }

            return query;
        }

        private IQueryable<ItemOverviewDto> SortByDescending(string column, IQueryable<ItemOverviewDto> query)
        {
            switch (column)
            {
                case "Name":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "ReplenishmentPeriod":
                    query = query.OrderByDescending(x => x.ReplenishmentPeriod);
                    break;
                case "NextReplenishmentDate":
                    query = query.OrderByDescending(x => x.NextReplenishmentDate);
                    break;
                case "LastReplenishmentQuantity":
                    query = query.OrderByDescending(x => x.LastReplenishmentQuantity);
                    break;
                default:
                    break;
            }

            return query;
        }

        private IQueryable<ItemOverviewDto> GetBaseQuery(string userId)
        {
            var query = _itemsRepository.All()
                .Where(x => x.UserId == userId)
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
                 });

            return query;
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

            if (itemToUpdate != null)
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
