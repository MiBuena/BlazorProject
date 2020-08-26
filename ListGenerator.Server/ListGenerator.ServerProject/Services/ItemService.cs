using ListGenerator.Common.Interfaces;
using ListGenerator.Common.Models;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Services
{
    public class ItemService : IItemService
    {
        private const string SaveItemSuccessMessage = "The item was saved successfully";
        private const string SaveItemErrorMessage = "An error occurred while saving the item";

        private readonly IApiClient _apiClient;
        private readonly IJsonHelper _jsonHelper;

        public ItemService(IApiClient apiClient, IJsonHelper jsonHelper)
        {
            _apiClient = apiClient;
            _jsonHelper = jsonHelper;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsOverviewModels()
        {
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemDto>>("api/items/overview");

            return dtos;
        }


        public async Task<ItemDto> GetItem(int id)
        {
            var dto = await _apiClient.GetAsync<ItemDto>($"api/items/{id}");
            return dto;
        }

        public async Task<ApiResponse> ReplenishItems(IEnumerable<ReplenishmentData> items)
        {
            var a = new ReplenishmentModel()
            {
                ReplenishmentData = items
            };

            var itemsJson = _jsonHelper.Serialize2(a);

            var response = await _apiClient.PostAsync("api/items/replenish", itemsJson, SaveItemSuccessMessage, SaveItemErrorMessage);

            return response;
        }



        public async Task<ApiResponse> AddItem(ItemViewModel item)
        {
            var itemJson = _jsonHelper.Serialize2(item);

            var response = await _apiClient.PostAsync("api/items", itemJson, SaveItemSuccessMessage, SaveItemErrorMessage);

            return response;
        }

        public async Task<ApiResponse> DeleteItem(int itemId)
        {
            var response = await _apiClient.DeleteAsync($"api/items/{itemId}");
            return response;
        }

        public async Task<ItemsOverviewResponse> GetAllItems()
        {
            var response = await _apiClient.GetItems("api/items");

            return response;
        }

        public async Task<ItemsOverviewResponse> GetShoppingListItems()
        {
            var response = await _apiClient.GetItems("api/items/shoppinglist");

            return response;
        }

        public async Task<ApiResponse> UpdateItem(ItemViewModel item)
        {
            var itemJson = _jsonHelper.Serialize2(item);

            var response = await _apiClient.PutAsync("api/items", itemJson, SaveItemSuccessMessage, SaveItemErrorMessage);

            return response;
        }

        public async Task<ItemsWithLastPurchaseReponse> GetItemsWithLastPurchases()
        {
            var response = await _apiClient.GetItemsWithLastPurchase($"api/items/itemswithlastpurchase");

            var datetime = DateTime.Now;

            
            return response;
        }
    }

    public class Aa
    {

    }
}
