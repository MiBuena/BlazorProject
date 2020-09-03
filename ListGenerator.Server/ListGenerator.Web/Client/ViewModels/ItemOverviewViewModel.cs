using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.Client.ViewModels
{
    public class ItemOverviewViewModel : ItemViewModel
    {
        public DateTime? LastReplenishmentDate { get; set; }

        public int? LastReplenishmentQuantity { get; set; }
    }
}
