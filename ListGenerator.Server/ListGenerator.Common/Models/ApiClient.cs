using ListGenerator.Common.Builders;
using ListGenerator.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Models
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
