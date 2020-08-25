using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Interfaces
{
    public interface IPurchasesRepository
    {
        IEnumerable<Purchase> GetItemsLastPurchases();
    }
}
