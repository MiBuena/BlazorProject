using AutoMapper;
using ListGenerator.Web.Client.Interfaces;
using ListGenerator.Web.Client.Builders;
using ListGenerator.Web.Shared.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Models
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

        public async Task<T> GetAsync<T>(string requestUri)
        {
            var httpResponse = await _httpClient.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var deserializedItems = _jsonHelper.Deserialize<T>(content);

            return deserializedItems;
        }

        public async Task<ApiResponse> PostAsync(string requestUri, string jsonContent, string errorMessage = null)
        {
            var stringContent =
              new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUri, stringContent);

            var apiReponse = ResponseBuilder.BuildApiResponse(response.IsSuccessStatusCode, errorMessage);

            return apiReponse;
        }

        public async Task<ApiResponse> PutAsync(string requestUri, string jsonContent, string errorMessage = null)
        {
            var stringContent =
              new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestUri, stringContent);

            var apiReponse = ResponseBuilder.BuildApiResponse(response.IsSuccessStatusCode, errorMessage);

            return apiReponse;
        }

        public async Task<ApiResponse> DeleteAsync(string requestUri, string errorMessage = null)
        {
            var response = await _httpClient.DeleteAsync(requestUri);

            var apiReponse = ResponseBuilder.BuildApiResponse(response.IsSuccessStatusCode, errorMessage);

            return apiReponse;
        }
    }
}
