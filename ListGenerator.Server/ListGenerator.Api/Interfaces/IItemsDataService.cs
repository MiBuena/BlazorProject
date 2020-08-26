﻿using ListGenerator.Api.Services;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Interfaces
{
    public interface IItemsDataService
    {
        IEnumerable<ItemOverviewDto> GetOverviewItemsModels();

        IEnumerable<ShoppingListItem> CalculateGenerationList();

        void ReplenishItems(IEnumerable<ReplenishmentData> replenishmentData);
    }
}
