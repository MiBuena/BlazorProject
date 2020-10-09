using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Responses;
using System.Collections.Generic;

namespace ListGenerator.Server.Interfaces
{
    public interface IItemsDataService
    {
        Response<IEnumerable<ItemNameDto>> GetItemsNames(string searchWord, string userId);

        Response<ItemDto> GetItem(int itemId);

        BaseResponse AddItem(string userId, ItemDto itemDto);

        BaseResponse UpdateItem(string userId, ItemDto itemDto);

        void DeleteItem(int id);

        IEnumerable<ItemDto> GetShoppingList(string secondReplenishmentDate, string userId);

        Response<ItemsOverviewPageDto> GetItemsOverviewPageModel(string userId, FilterPatemetersDto dto);
    }
}
