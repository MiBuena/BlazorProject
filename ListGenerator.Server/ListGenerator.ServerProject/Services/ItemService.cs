using ListGenerator.Common.Interfaces;
using ListGenerator.Common.Models;
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


        public async Task<ApiResponse> AddItem(ItemViewModel item)
        {
            var itemJson = _jsonHelper.Serialize(item);

            var response = await _apiClient.PostAsync("api/items", itemJson, SaveItemSuccessMessage, SaveItemErrorMessage);

            return response;
        }

        public async Task<ItemsOverviewResponse> GetAllItems()
        {
            var response = await _apiClient.GetAllItems("api/items");

            return response;
        }

        public async Task<GetItemResponse> GetItem(int id)
        {
            var response = await _apiClient.GetItem($"api/items/{id}");
            return response;
        }

        public async Task<ApiResponse> UpdateItem(ItemViewModel item)
        {
            var itemJson = _jsonHelper.Serialize(item);

            var response = await _apiClient.PutAsync("api/items", itemJson, SaveItemSuccessMessage, SaveItemErrorMessage);

            return response;
        }
    }
}
