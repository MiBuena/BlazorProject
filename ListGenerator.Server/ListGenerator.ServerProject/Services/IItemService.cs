﻿using ListGenerator.Common.Models;
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
        Task<GetItemResponse> GetItem(int id);

        Task<ApiResponse> AddItem(ItemViewModel item);

        Task<ApiResponse> UpdateItem(ItemViewModel item);

        Task<ItemsOverviewResponse> GetAllItems();
    }
}
