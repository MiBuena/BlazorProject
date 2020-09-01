using ListGenerator.Models;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Server.Interfaces
{
    public interface IItemsDataService
    {
        IEnumerable<ItemOverviewDto> GetOverviewItemsModels(string userId);

        ItemDto GetItem(int itemId);

        int AddItem(string userId, ItemDto itemDto);

        void UpdateItem(string userId, ItemDto itemDto);

        void DeleteItem(int id);     

        IEnumerable<ItemDto> GetShoppingList(string secondReplenishmentDate, string userId);
    }
}
