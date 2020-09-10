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
        Table<T> Sort<T>(int id, Table<T> table);
        Table<T> GetTable<T>(IEnumerable<T> items);
    }
}
