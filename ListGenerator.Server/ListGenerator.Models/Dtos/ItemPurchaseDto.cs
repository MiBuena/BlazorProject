using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ItemPurchaseDto
    {
        public ItemDto Item { get; set; }

        public PurchaseDto Purchase { get; set; }
    }
}
