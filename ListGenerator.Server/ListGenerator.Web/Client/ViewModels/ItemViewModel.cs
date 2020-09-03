using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListGenerator.Web.Client.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(2)]
        public string ReplenishmentPeriod { get; set; } = "1";

        [Required]
        public DateTime NextReplenishmentDate { get; set; }
    }
}
