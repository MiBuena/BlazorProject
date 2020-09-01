using ListGenerator.Web.Shared.Dtos;
using System.Collections.Generic;

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
