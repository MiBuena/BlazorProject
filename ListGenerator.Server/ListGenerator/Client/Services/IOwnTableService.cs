using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Services
{
    public interface IOwnTableService
    {
        Table<T> Sort<T>(int id, Table<T> table);
        Table<ItemOverviewViewModel> GetItemsOverviewTable(IEnumerable<ItemOverviewViewModel> items);
    }
}
