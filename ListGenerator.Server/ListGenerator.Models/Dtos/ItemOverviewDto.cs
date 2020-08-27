﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ItemOverviewDto : ItemDto
    {
        public DateTime NextReplenishmentDate { get; set; }

        public DateTime? LastReplenishmentDate { get; set; }

        public int? LastReplenishmentQuantity { get; set; }
    }
}
