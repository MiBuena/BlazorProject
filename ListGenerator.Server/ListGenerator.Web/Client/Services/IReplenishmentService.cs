using ListGenerator.Web.Shared.Dtos;
using System.Threading.Tasks;
using ListGenerator.Web.Client.Models;

namespace ListGenerator.Web.Client.Services
{
    public interface IReplenishmentService
    {
        Task<ApiResponse> ReplenishItems(ReplenishmentDto replenishmentModel);
    }
}
