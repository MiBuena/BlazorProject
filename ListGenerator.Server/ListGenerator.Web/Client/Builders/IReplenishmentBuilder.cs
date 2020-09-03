using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using System;
using System.Collections.Generic;

namespace ListGenerator.Web.Client.Builders
{
    public interface IReplenishmentBuilder
    {
        ReplenishmentDto BuildReplenishmentDto(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, PurchaseItemViewModel viewModel);
        
        ReplenishmentDto BuildReplenishmentDto(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, List<PurchaseItemViewModel> viewModels);
    }
}
