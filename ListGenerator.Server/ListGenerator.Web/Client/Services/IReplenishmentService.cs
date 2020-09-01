using ListGenerator.Web.Shared.Models;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Web.Client.Models;

namespace ListGenerator.Web.Client.Services
{
    public interface IReplenishmentService
    {
        Task<ApiResponse> ReplenishItems(ReplenishmentDto replenishmentModel);
    }
}
