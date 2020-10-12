using AutoMapper;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Shared.Dtos;
using System.Threading.Tasks;
using ListGenerator.Client.Interfaces;
using ListGenerator.Client.Models;
using ListGenerator.Client.Services;
using ListGenerator.Shared.Responses;

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

        public async Task<BaseResponse> ReplenishItems(ReplenishmentDto replenishmentModel)
        {
            var replenishmentJson = _jsonHelper.Serialize(replenishmentModel);

            var response = await _apiClient.PostAsync("api/replenishment/replenish", replenishmentJson);

            return response;
        }
    }
}
