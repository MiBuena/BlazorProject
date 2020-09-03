using AutoMapper;
using ListGenerator.Web.Shared.Interfaces;
using ListGenerator.Web.Shared.Dtos;
using System.Threading.Tasks;
using ListGenerator.Web.Client.Interfaces;
using ListGenerator.Web.Client.Models;

namespace ListGenerator.Web.Client.Services
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

        public async Task<ApiResponse> ReplenishItems(ReplenishmentDto replenishmentModel)
        {
            var replenishmentJson = _jsonHelper.Serialize(replenishmentModel);

            var response = await _apiClient.PostAsync("api/replenishment/replenish", replenishmentJson, null);

            return response;
        }
    }
}
