using ListGenerator.Api.DB;
using ListGenerator.Api.Interfaces;
using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Repositories
{
    public class PurchasesRepository : IPurchasesRepository
    {
        private readonly ListGenerationContext _context;

        public PurchasesRepository(ListGenerationContext context)
        {
            _context = context;
        }

        public IEnumerable<Purchase> GetItemsLastPurchases()
        {
            var a = _context.Items
                .Select(x => x.Purchases.OrderByDescending(y => y.Replenishment.Date).FirstOrDefault()).ToList();


            return a;
        }
    }
}
