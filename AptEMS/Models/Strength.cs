using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Strength
    {
        [Key]
        public int StrengthID { get; set; }

        [Required]
        [StringLength(255)]
        public string StrengthName { get; set; }

        public bool IsActive { get; set; }
    }
}