using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class ItemOverviewDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double ReplenishmentPeriod { get; set; }
    }
}
