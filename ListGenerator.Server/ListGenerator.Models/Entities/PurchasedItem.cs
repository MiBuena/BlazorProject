using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListGenerator.Models.Entities
{
    public class PurchasedItem
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }

        public int ReplenishmentId { get; set; }

        public Replenishment Replenishment { get; set; }
    }
}
