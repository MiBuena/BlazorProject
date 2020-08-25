using ListGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Interfaces
{
    public interface IApiClient
    {
        Task<ApiResponse> PostAsync(string requestUri, string jsonContent, string successMessage = null, string errorMessage = null);

        Task<ApiResponse> PutAsync(string requestUri, string jsonContent, string successMessage = null, string errorMessage = null);

        Task<ItemsOverviewResponse> GetAllItems(string requestUri);
        Task<GetItemResponse> GetItem(string requestUri);

    }
}
