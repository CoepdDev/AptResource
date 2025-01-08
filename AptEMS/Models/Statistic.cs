using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    [Table("Statistic")]
    public class Statistic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IconType { get; set; } // Fixed icons like Placement, Clients, etc.

        [Required]
        public int Count { get; set; } // Dynamic count

        [Required]
        public string Label { get; set; } // Label like "Successful Placements"

        public bool IsActive { get; set; } // Status for visibility
    }
}