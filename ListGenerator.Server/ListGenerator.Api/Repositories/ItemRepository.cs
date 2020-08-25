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

        public Item UpdateItem(Item item)
        {
            var foundItem = _context.Items.FirstOrDefault(e => e.Id == item.Id);

            if (foundItem != null)
            {
                foundItem.Name = item.Name;
                foundItem.ReplenishmentPeriod = foundItem.ReplenishmentPeriod;

                _context.SaveChanges();

                return foundItem;
            }

            return null;
        }


        public Item GetItemById(int itemId)
        {
            return _context.Items.FirstOrDefault(c => c.Id == itemId);
        }

        public IEnumerable<Item> GetAllItems()
        {
            var items = _context.Items.ToList();
            return items;
        }
    }
}
