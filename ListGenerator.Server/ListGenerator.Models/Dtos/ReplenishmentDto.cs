using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ReplenishmentDto
    {
        public ICollection<PurchaseItemDto> Purchaseitems { get; set; } = new List<PurchaseItemDto>();
    }
}
