using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptEMS.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey("ClientAgreement")]
        public int AgreementID { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now; // Matches the default value in the table

        [Required]
        [Column(TypeName = "decimal")] // Remove precision definition if mismatched
        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }

       

        [Required]
        [StringLength(50)]
        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }

        [StringLength(50)]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [StringLength(100)]
        [Display(Name = "Transaction ID")]
        public string TransactionID { get; set; }

        [StringLength(50)]
        [Display(Name = "Payment Gateway")]
        public string PaymentGateway { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; }


        // Navigation property to link with ClientAgreement
        public virtual ClientAgreements ClientAgreement { get; set; }
    }
}
