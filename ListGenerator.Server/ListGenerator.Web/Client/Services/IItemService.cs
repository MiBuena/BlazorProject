using ListGenerator.Web.Client.Models;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemOverviewDto>> GetItemsOverviewModels();
        Task<ItemDto> GetItem(int id);
        Task<IEnumerable<ItemDto>> GetShoppingListItems(DateTime secondReplenishmentDate);

        Task<ApiResponse> AddItem(ItemViewModel item);

        Task<ApiResponse> UpdateItem(ItemViewModel item);

        Task<ApiResponse> DeleteItem(int item);     
    }
}
