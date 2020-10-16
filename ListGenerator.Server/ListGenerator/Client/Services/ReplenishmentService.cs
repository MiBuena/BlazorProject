using AutoMapper;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Shared.Dtos;
using System.Threading.Tasks;
using ListGenerator.Client.Interfaces;
using ListGenerator.Client.Models;
using ListGenerator.Client.Services;
using ListGenerator.Shared.Responses;
using System.Collections.Generic;
using ListGenerator.Shared.Helpers;
using System;

namespace ListGenerator.Client.Services
{
    public class ReplenishmentService : IReplenishmentService
    {
        private readonly IApiClient _apiClient;
        private readonly IJsonHelper _jsonHelper;

        public ReplenishmentService(IApiClient apiClient, IJsonHelper jsonHelper)
        {
            _apiClient = apiClient;
            _jsonHelper = jsonHelper;
        }


        public async Task<Response<IEnumerable<ItemDto>>> GetShoppingListItems(DateTime secondReplenishmentDate)
        {
            var invariantDate = DateTimeHelper.ToTransferDateAsString(secondReplenishmentDate);
            var response = await _apiClient.GetAsync<Response<IEnumerable<ItemDto>>>($"api/replenishment/shoppinglist/{invariantDate}");

            return response;
        }

        public async Task<BaseResponse> ReplenishItems(ReplenishmentDto replenishmentModel)
        {
            var replenishmentJson = _jsonHelper.Serialize(replenishmentModel);

            var response = await _apiClient.PostAsync("api/replenishment/replenish", replenishmentJson);

            return response;
        }
    }
}
