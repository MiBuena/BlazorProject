using ListGenerator.Api.DB;
using ListGenerator.Api.Interfaces;
using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ListGenerationContext _context;

        public ItemRepository(ListGenerationContext context)
        {
            _context = context;
        }
        
        public Item AddItem(Item item)
        {
            var addedEntity = _context.Items.Add(item);
            _context.SaveChanges();
            return addedEntity.Entity;
        }
    }
}
