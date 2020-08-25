using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListGenerator.Models.Entities
{
    public class Replenishment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
