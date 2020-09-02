using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using System;

namespace ListGenerator.Web.Client.Builders
{
    public interface IReplenishmentBuilder
    {
        ReplenishmentDto BuildReplenishmentDto(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, PurchaseItemViewModel viewModel);
    }
}
