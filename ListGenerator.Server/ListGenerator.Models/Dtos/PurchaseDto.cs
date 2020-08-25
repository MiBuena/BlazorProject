using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public int Quantity { get; set; }
    }
}
