using AutoMapper;
using ListGenerator.Client.Models;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListGenerator.Client.Interfaces;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Shared.Enums;
using ListGenerator.Shared.Constants;
using ListGenerator.Shared.Helpers;

namespace ListGenerator.Client.Services
{
    public class ItemService : IItemService
    {
        private const string SaveItemSuccessMessage = "The item was saved successfully";
        private const string SaveItemErrorMessage = "An error occurred while saving the item";

        private readonly IApiClient _apiClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IMapper _mapper;

        public ItemService(IApiClient apiClient, IJsonHelper jsonHelper, IMapper mapper)
        {
            _apiClient = apiClient;
            _jsonHelper = jsonHelper;
            _mapper = mapper;
        }

        public async Task<ItemsOverviewPageDto> GetItemsOverviewPageModel(int? pageSize, int? skipItems, string orderBy, string searchWord, DateTime? searchDate)
        {
            var sortingData = GetSortingData(orderBy);

            var dateToString = DateTimeHelper.ToTransferDateAsString(searchDate);    

            var dto = await _apiClient.GetAsync<ItemsOverviewPageDto>($"api/items/overview/?PageSize={pageSize}&SkipItems={skipItems}&OrderByColumn={sortingData.OrderByColumn}&OrderByDirection={sortingData.OrderByDirection}&SearchWord={searchWord}&SearchDate={dateToString}");

            return dto;
        }

        private SortingData GetSortingData(string orderBy)
        {
            var sortingData = new SortingData();

            if (orderBy != null)
            {
                sortingData.OrderByColumn = orderBy.Split(" ")[0];

                sortingData.OrderByDirection = orderBy.Split(" ")[1] == Constants.GridAscendingKeyword
                    ? SortingDirection.Ascending 
                    : sortingData.OrderByDirection = SortingDirection.Descending;
            }

            return sortingData;
        }

        public async Task<ItemDto> GetItem(int id)
        {
            var dto = await _apiClient.GetAsync<ItemDto>($"api/items/{id}");
            return dto;
        }

        public async Task<ApiResponse> AddItem(ItemViewModel item)
        {
            var itemDto = _mapper.Map<ItemViewModel, ItemDto>(item);

            var itemJson = _jsonHelper.Serialize(itemDto);

            var response = await _apiClient.PostAsync("api/items", itemJson, SaveItemErrorMessage);

            return response;
        }

        public async Task<ApiResponse> UpdateItem(ItemViewModel item)
        {
            var itemDto = _mapper.Map<ItemViewModel, ItemDto>(item);

            var itemJson = _jsonHelper.Serialize(itemDto);

            var response = await _apiClient.PutAsync("api/items", itemJson, SaveItemErrorMessage);

            return response;
        }

        public async Task<ApiResponse> DeleteItem(int itemId)
        {
            var response = await _apiClient.DeleteAsync($"api/items/{itemId}");
            return response;
        }

        public async Task<IEnumerable<ItemDto>> GetShoppingListItems(DateTime secondReplenishmentDate)
        {
            var invariantDate = DateTimeHelper.ToTransferDateAsString(secondReplenishmentDate);
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemDto>>($"api/items/shoppinglist/{invariantDate}");

            return dtos;
        }
    }
}
