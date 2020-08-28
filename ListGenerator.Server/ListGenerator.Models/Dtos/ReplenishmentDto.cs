﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ReplenishmentDto
    {
        public DateTime FirstReplenishmentDate { get; set; }

        public DateTime SecondReplenishmentDate { get; set; }

        public ICollection<PurchaseItemDto> Purchaseitems { get; set; } = new List<PurchaseItemDto>();
    }
}
