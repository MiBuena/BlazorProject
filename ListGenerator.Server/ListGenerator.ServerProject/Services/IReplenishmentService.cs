using ListGenerator.Common.Models;
using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Services
{
    public interface IReplenishmentService
    {
        Task<ApiResponse> ReplenishItems(IEnumerable<PurchaseItemViewModel> items);
    }
}
