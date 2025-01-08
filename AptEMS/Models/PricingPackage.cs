using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class PricingPackage
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        [StringLength(100)]
        public string PackageName { get; set; }

        [Required]
        [DataType(DataType.Currency)] 
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string BestFor { get; set; }

        // Controls visibility on the BuyNow page
        public bool IsActive { get; set; }

        public string BillingCycle { get; set; } // "Monthly" or "Yearly"
    }
}
