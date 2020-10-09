using ListGenerator.Client.Models;
using System.Threading.Tasks;

namespace ListGenerator.Client.Interfaces
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string requestUri);

        Task<T> PostAsync<T>(string requestUri, string jsonContent, string errorMessage = null);

        Task<T> PutAsync<T>(string requestUri, string jsonContent, string errorMessage = null);

        Task<ApiResponse> DeleteAsync(string requestUri, string errorMessage = null);
    }
}
