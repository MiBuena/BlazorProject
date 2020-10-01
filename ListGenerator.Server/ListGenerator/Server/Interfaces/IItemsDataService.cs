using ListGenerator.Shared.Dtos;
using System.Collections.Generic;

namespace ListGenerator.Server.Interfaces
{
    public interface IItemsDataService
    {
        IEnumerable<ItemNameDto> GetItemsNames(string searchWord, string userId);

        ItemDto GetItem(int itemId);

        int AddItem(string userId, ItemDto itemDto);

        void UpdateItem(string userId, ItemDto itemDto);

        void DeleteItem(int id);

        IEnumerable<ItemDto> GetShoppingList(string secondReplenishmentDate, string userId);
        
        ItemsOverviewPageDto GetItemsOverviewPageModel(string userId, FilterPatemetersDto dto);
    }
}
