using ListGenerator.Common.Models;
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
        Task<ApiResponse> AddItem(ItemViewModel item);

        Task<ItemsOverviewResponse> GetAllItems();
    }
}
