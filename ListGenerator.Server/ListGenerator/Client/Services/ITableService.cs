using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Services
{
    public interface ITableService
    {
        Table Sort(int id, IEnumerable<ItemOverviewViewModel> items);
        Table GetTable(IEnumerable<ItemOverviewViewModel> items);
    }
}
