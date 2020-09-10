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
        List<OverviewTableHeading> GetSortingDirections();
        Table Sort(int id, IEnumerable<ItemOverviewViewModel> items);
        List<Heading> GetItemsOverviewHeadings();
    }
}
