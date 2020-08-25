using ListGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Interfaces
{
    public interface IApiClient
    {
        Task<ItemsOverviewResponse> GetItems(string requestUri);
        Task<GetItemResponse> GetItem(string requestUri);

        Task<ApiResponse> PostAsync(string requestUri, string jsonContent, string successMessage = null, string errorMessage = null);

        Task<ApiResponse> PutAsync(string requestUri, string jsonContent, string successMessage = null, string errorMessage = null);

        Task<ApiResponse> DeleteAsync(string requestUri, string errorMessage = null);

        Task<ItemsWithLastPurchaseReponse> GetItemsWithLastPurchase(string requestUri);


    }
}
