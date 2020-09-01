using AutoMapper;
using ListGenerator.Common.Interfaces;
using ListGenerator.Common.Models;
using ListGenerator.Models;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Services
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

        public async Task<IEnumerable<ItemOverviewDto>> GetItemsOverviewModels()
        {
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemOverviewDto>>("api/items/overview");

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
