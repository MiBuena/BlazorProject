using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models
{
    public class ReplenishmentData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; } = "1";
    }
}
