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

        public IEnumerable<Purchase> GetAllPurchases()
        {
            var items = _context.Purchases.ToList();
            return items;
        }

        public IEnumerable<Purchase> GetAllPurchasesByItemId(int itemId)
        {
            var items = _context.Purchases.Where(x=>x.ItemId == itemId);
            return items;
        }
    }
}
