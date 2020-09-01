using ListGenerator.Web.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Interfaces
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string requestUri);

        Task<ApiResponse> PostAsync(string requestUri, string jsonContent, string errorMessage = null);

        Task<ApiResponse> PutAsync(string requestUri, string jsonContent, string errorMessage = null);

        Task<ApiResponse> DeleteAsync(string requestUri, string errorMessage = null);
    }
}
