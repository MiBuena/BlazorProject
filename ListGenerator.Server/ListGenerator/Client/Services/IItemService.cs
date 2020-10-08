using ListGenerator.Client.Models;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Shared.Responses;

namespace ListGenerator.Client.Services
{
    public interface IItemService
    {
        Task<Response<IEnumerable<ItemNameDto>>> GetItemsNames(string searchWord);
        Task<Response<ItemsOverviewPageDto>> GetItemsOverviewPageModel(int? pageSize, int? skipItems, string orderBy, string searchWord, DateTime? searchDate);
        
        Task<ItemDto> GetItem(int id);
        
        Task<IEnumerable<ItemDto>> GetShoppingListItems(DateTime secondReplenishmentDate);

        Task<ApiResponse> AddItem(ItemViewModel item);

        Task<ApiResponse> UpdateItem(ItemViewModel item);

        Task<ApiResponse> DeleteItem(int item);     
    }
}
