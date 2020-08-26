using ListGenerator.Common.Models;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetItemsOverviewModels();
        Task<ItemDto> GetItem(int id);
        Task<IEnumerable<ItemDto>> GetShoppingListItems();

        Task<ApiResponse> AddItem(ItemViewModel item);

        Task<ApiResponse> UpdateItem(ItemViewModel item);

        Task<ApiResponse> DeleteItem(int item);     
    }
}
