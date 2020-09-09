using ListGenerator.Shared.Dtos;
using System.Threading.Tasks;
using ListGenerator.Client.Models;

namespace ListGenerator.Client.Services
{
    public interface IReplenishmentService
    {
        Task<ApiResponse> ReplenishItems(ReplenishmentDto replenishmentModel);
    }
}
