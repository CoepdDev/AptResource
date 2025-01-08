using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Loan
    {
        [Required]
        public int Empid { get; set; }

        [Required]

        [Display(Name = "Employee Name")]
        public string FirstName { get; set; }

        public string SalaryTransactionID { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime ApprovalDate { get; set; }



        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Loan Sanction Amount must be a valid number.")]
        public string LoanSanctionAmount { get; set; }


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Tenure must be a valid number.")]
        public string Tenure { get; set; }


        [Required]

        [Range(0, int.MaxValue, ErrorMessage = "EMI Amount must be a valid number.")]
        public string EMIAmount { get; set; }
        public string LoanSourceBank { get; set; }
        public string LoanCreditedBy { get; set; }
    }
}