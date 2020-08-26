using ListGenerator.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Models
{
    public class ApiClientNew
    {
        private readonly HttpClient _httpClient;
        private readonly JsonHelperNew _jsonHelper = new JsonHelperNew();

        public ApiClientNew(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            var httpResponse = await _httpClient.GetAsync(requestUri);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var deserializedItems = _jsonHelper.Deserialize<T>(content);

            return deserializedItems;
        }
    }
}
