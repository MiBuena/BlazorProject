using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Interfaces
{
    public interface IItemRepository
    {
        Item GetItemById(int itemId);
        IEnumerable<Item> GetAllItems();

        Item AddItem(Item item);
        
        Item UpdateItem(Item item);

        void DeleteItem(int itemId);
    }
}
