using ListGenerator.Common.Interfaces;
using ListGenerator.Common.Models;
using ListGenerator.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Server.Services
{
    public class ItemService : IItemService
    {
        private readonly IApiClient _apiClient;
        private readonly IJsonHelper _jsonHelper;

        public ItemService(IApiClient apiClient, IJsonHelper jsonHelper)
        {
            _apiClient = apiClient;
            _jsonHelper = jsonHelper;
        }


        public async Task<ApiResponse> AddItem(Item item)
        {
            var employeeJson = _jsonHelper.Serialize(item);

            var response = await _apiClient.PostAsync("api/employee", employeeJson);

            return response;
        }
    }
}
