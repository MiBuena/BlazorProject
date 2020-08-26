using AutoMapper;
using ListGenerator.Common.Interfaces;
using ListGenerator.Common.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Services
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

        public async Task<ApiResponse> ReplenishItems(IEnumerable<PurchaseItemViewModel> items)
        {
            var replenishmentModel = new ReplenishmentDto()
            {
                Purchaseitems = items.Select(x => _mapper.Map<PurchaseItemViewModel, PurchaseItemDto>(x))
            };

            var replenishmentJson = _jsonHelper.Serialize(replenishmentModel);

            var response = await _apiClient.PostAsync("api/replenishment/replenish", replenishmentJson, null);

            return response;
        }
    }
}
