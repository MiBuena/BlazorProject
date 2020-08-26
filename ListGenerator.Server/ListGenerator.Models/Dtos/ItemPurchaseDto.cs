using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ItemPurchaseDto
    {
        public ItemOverviewDto Item { get; set; }

        public PurchaseDto Purchase { get; set; }
    }
}
