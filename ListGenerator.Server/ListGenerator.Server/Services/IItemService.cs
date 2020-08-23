using ListGenerator.Common.Models;
using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Server.Services
{
    public interface IItemService
    {
        Task<ApiResponse> AddItem(Item item);
    }
}
