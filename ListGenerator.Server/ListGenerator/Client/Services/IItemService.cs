using ListGenerator.Client.Models;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Services
{
    public interface IItemService
    {
        IEnumerable<ItemOverviewViewModel> ApplyFilters(string searchWord, DateTime? searchDate, IEnumerable<ItemOverviewViewModel> originalItems);
        
        Task<ItemsOverviewPageDto> GetItemsOverviewPageModel(int? pageSize, int? skipItems, string orderBy);
        
        Task<ItemDto> GetItem(int id);
        
        Task<IEnumerable<ItemDto>> GetShoppingListItems(DateTime secondReplenishmentDate);

        Task<ApiResponse> AddItem(ItemViewModel item);

        Task<ApiResponse> UpdateItem(ItemViewModel item);

        Task<ApiResponse> DeleteItem(int item);     
    }
}
