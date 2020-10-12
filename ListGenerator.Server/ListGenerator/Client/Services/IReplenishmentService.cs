using ListGenerator.Shared.Dtos;
using System.Threading.Tasks;
using ListGenerator.Client.Models;
using ListGenerator.Shared.Responses;

namespace ListGenerator.Client.Services
{
    public interface IReplenishmentService
    {
        Task<BaseResponse> ReplenishItems(ReplenishmentDto replenishmentModel);
    }
}
