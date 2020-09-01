using AutoMapper;
using ListGenerator.Common.Interfaces;
using ListGenerator.Common.Models;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Services
{
    public class ReplenishmentService : IReplenishmentService
    {
        private readonly IApiClient _apiClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IMapper _mapper;

        public ReplenishmentService(IApiClient apiClient, IJsonHelper jsonHelper, IMapper mapper)
        {
            _apiClient = apiClient;
            _jsonHelper = jsonHelper;
            _mapper = mapper;
        }

        public async Task<ApiResponse> ReplenishItems(ReplenishmentDto replenishmentModel)
        {
            var replenishmentJson = _jsonHelper.Serialize(replenishmentModel);

            var response = await _apiClient.PostAsync("api/replenishment/replenish", replenishmentJson, null);

            return response;
        }
    }
}
