using AutoMapper;
using ListGenerator.Client.Models;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListGenerator.Client.Interfaces;
using ListGenerator.Shared.Interfaces;
using System.Linq;
using ListGenerator.Client.Enums;

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

        public IEnumerable<ItemOverviewViewModel> ApplyFilters(string searchWord, DateTime? searchDate, IEnumerable<ItemOverviewViewModel> originalItems)
        {
            var items = FilterBySearchWord(searchWord, originalItems);

            items = FilterByDate(searchDate, items);

            return items;
        }

        private IEnumerable<ItemOverviewViewModel> FilterByDate(DateTime? searchDate, IEnumerable<ItemOverviewViewModel> items)
        {
            if (searchDate == null)
            {
                return items;
            }

            var result = items
                .Where(x => x.NextReplenishmentDate.Date == searchDate.Value.Date
                || x.LastReplenishmentDate == searchDate.Value.Date);

            return result;
        }

        private IEnumerable<ItemOverviewViewModel> FilterBySearchWord(string searchWord, IEnumerable<ItemOverviewViewModel> items)
        {
            if (searchWord == null)
            {
                searchWord = string.Empty;
            }

            var result = items
            .Where(x =>
               x.Name.ToLower().Contains(searchWord.ToLower())
            || x.ReplenishmentPeriodString == searchWord
            || (x.LastReplenishmentQuantity.HasValue ? x.LastReplenishmentQuantity.Value.ToString() == searchWord : false));

            return result;
        }

        public async Task<IEnumerable<ItemOverviewDto>> GetItemsOverviewModels(int? skip, int? top, string orderBy)
        {
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemOverviewDto>>($"api/items/overview/{skip}/{top}/{orderBy}");

            return dtos;
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
            var internationalDateTime = secondReplenishmentDate.ToString("dd-MM-yyyy");
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemDto>>($"api/items/shoppinglist/{internationalDateTime}");

            return dtos;
        }
    }
}
