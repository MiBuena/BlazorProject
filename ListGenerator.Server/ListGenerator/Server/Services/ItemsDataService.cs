using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ListGenerator.Data.Interfaces;
using ListGenerator.Data.Entities;
using System.Linq.Expressions;
using System.Globalization;
using ListGenerator.Server.Extensions;
using ListGenerator.Shared.Enums;
using ListGenerator.Shared.Helpers;
using ListGenerator.Shared.Responses;
using ListGenerator.Client.Builders;
using ListGenerator.Server.Builders;

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

        public Response<IEnumerable<ItemNameDto>> GetItemsNames(string searchWord, string userId)
        {
            try
            {
                var query = _itemsRepository.All()
                    .Where(x => x.UserId == userId);

                var names = new List<ItemNameDto>();

                if (!string.IsNullOrEmpty(searchWord))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchWord.ToLower()));
                }

                names = _mapper.ProjectTo<ItemNameDto>(query).ToList();

                var response = Builders.ResponseBuilder.Success<IEnumerable<ItemNameDto>>(names);
                return response;
            }
            catch (Exception ex)
            {
                var response = Builders.ResponseBuilder.Failure<IEnumerable<ItemNameDto>>("An error occured while getting items names.");
                return response;
            }
        }

        public Response<ItemsOverviewPageDto> GetItemsOverviewPageModel(string userId, FilterPatemetersDto dto)
        {
            try
            {
                var query = GetOverviewItemsQuery(userId, dto);

                var dtos = query
                    .Skip(dto.SkipItems.Value)
                    .Take(dto.PageSize.Value)
                    .ToList();

                var itemsCount = query.Count();

                var pageDto = new ItemsOverviewPageDto()
                {
                    OverviewItems = dtos,
                    TotalItemsCount = itemsCount
                };

                var response = Builders.ResponseBuilder.Success(pageDto);
                return response;
            }
            catch (Exception ex)
            {
                var response = Builders.ResponseBuilder.Failure<ItemsOverviewPageDto>("An error occured while getting items names.");
                return response;
            }
        }

        private IQueryable<ItemOverviewDto> GetOverviewItemsQuery(string userId, FilterPatemetersDto dto)
        {
            var query = GetBaseQuery(userId);

            query = FilterBySearchWord(dto.SearchWord, query);

            query = FilterBySearchDate(dto.SearchDate, query);

            query = Sort(dto.OrderByColumn, dto.OrderByDirection, query);

            return query;
        }

        private IQueryable<ItemOverviewDto> FilterBySearchDate(string searchDate, IQueryable<ItemOverviewDto> query)
        {
            if (searchDate != null)
            {
                var parsedDate = DateTimeHelper.ToDateFromTransferDateAsString(searchDate);

                query = query
                    .Where(x => x.LastReplenishmentDate == parsedDate
                || x.NextReplenishmentDate == parsedDate);
            }

            return query;
        }

        private IQueryable<ItemOverviewDto> FilterBySearchWord(string searchWord, IQueryable<ItemOverviewDto> query)
        {
            if (searchWord != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchWord.ToLower()));
            }

            return query;
        }

        private IQueryable<ItemOverviewDto> Sort(string orderByColumn, SortingDirection? orderByDirection, IQueryable<ItemOverviewDto> query)
        {
            if (orderByColumn != null && orderByDirection != null)
            {
                if (orderByDirection == SortingDirection.Ascending)
                {
                    query = query.OrderByProperty(orderByColumn);
                }
                else
                {
                    query = query.OrderByPropertyDescending(orderByColumn);
                }
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

        public Response<ItemDto> GetItem(int itemId)
        {
            try
            {
                var query = _itemsRepository.All().Where(x => x.Id == itemId);

                var dto = _mapper.ProjectTo<ItemDto>(query).FirstOrDefault();

                var response = Builders.ResponseBuilder.Success(dto);
                return response;
            }
            catch(Exception ex)
            {
                var response = Builders.ResponseBuilder.Failure<ItemDto>("An error occured while getting item");
                return response;
            }
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
            var date = DateTimeHelper.ToDateFromTransferDateAsString(secondReplenishmentDate);

            var query = _itemsRepository.All()
                .Where(x => x.NextReplenishmentDate.Date < date
                && x.UserId == userId)
                .OrderBy(x => x.NextReplenishmentDate);

            var itemsNeedingReplenishment = _mapper.ProjectTo<ItemDto>(query).ToList();

            return itemsNeedingReplenishment;
        }
    }
}
