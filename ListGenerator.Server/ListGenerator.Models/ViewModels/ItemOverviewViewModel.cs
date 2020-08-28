using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Models.ViewModels
{
    public class ItemOverviewViewModel : ItemViewModel
    {
        public DateTime NextReplenishmentDate { get; set; }

        public DateTime? LastReplenishmentDate { get; set; }

        public int? LastReplenishmentQuantity { get; set; }
    }
}
