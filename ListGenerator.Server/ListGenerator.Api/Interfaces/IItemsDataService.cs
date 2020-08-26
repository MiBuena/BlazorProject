using ListGenerator.Api.Services;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Interfaces
{
    public interface IItemsDataService
    {
        IEnumerable<ItemDto> GetOverviewItemsModels();

        ItemDto GetItem(int itemId);

        int AddItem(ItemDto itemDto);

        void UpdateItem(ItemDto itemDto);

        void DeleteItem(int id);     

        IEnumerable<ItemDto> GetShoppingList();

        void ReplenishItems(IEnumerable<ReplenishmentData> replenishmentData);
    }
}
