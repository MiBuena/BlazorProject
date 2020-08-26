using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double ReplenishmentPeriod { get; set; }

        public DateTime NextReplenishmentDate { get; set; }

        public DateTime? LastReplenishmentDate { get; set; }
    }
}
