using AutoMapper;
using ListGenerator.Common.Builders;
using ListGenerator.Common.Interfaces;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Models
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IMapper _mapper;

        public ApiClient(HttpClient httpClient, IJsonHelper jsonHelper, IMapper mapper)
        {
            _httpClient = httpClient;
            _jsonHelper = jsonHelper;
            _mapper = mapper;
        }

        public async Task<ItemsOverviewResponse> GetAllItems(string requestUri)
        {
            var a = await _httpClient.GetStreamAsync(requestUri);

            var b = await _jsonHelper.Deserialize<IEnumerable<Item>>(a);

            var viewModels = b.Select(x=> _mapper.Map<Item, ItemViewModel>(x)).ToList();

            var response = ResponseBuilder.BuildItemsOverviewResponse(viewModels);

            return response;
        }
        
        public async Task<ApiResponse> PostAsync(string requestUri, string jsonContent, string successMessage = null, string errorMessage = null)
        {
            var stringContent =
              new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUri, stringContent);

            var apiReponse = ResponseBuilder.BuildApiResponse(response.IsSuccessStatusCode, successMessage, errorMessage);
            
            return apiReponse;
        }
    }
}
