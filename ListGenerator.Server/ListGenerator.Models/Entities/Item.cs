using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListGenerator.Models.Entities
{ 
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public int ReplenishmentPeriod { get; set; }
    }
}
