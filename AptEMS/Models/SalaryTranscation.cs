using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class SalaryTranscation
    {

        [Required]


        [Display(Name = "Employee ID")]
        public int Empid { get; set; }

        [Required]

        [Display(Name = "AccountHolder Name")]
        public string FirstName { get; set; }

        [Required]

        [Display(Name = "Account Number")]
        public string BankAccountNumber { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }


        public string Leaves { get; set; }

        [Display(Name = "Loss Of Pay")]
        public string LOP { get; set; }


        [Display(Name = "Loan Amount")]
        public string LoanAmount { get; set; } 

        [Required]
        [Display(Name = "Working Days")]
        public string WorkingDays { get; set; }

        [Required]
        [Display(Name = "Net Salary")]
        public string Netsalary { get; set; }

        [Display(Name = "Apprisial Points ")]
        public string ApprisialPoints { get; set; }

        [Display(Name = "Warnig Points ")]
        public string WarnigPoints { get; set; }


        [Display(Name = "Delay Points")]

        public string DelayPoints { get; set; }

        [Display(Name = "Bonus")]

        public string Bonus { get; set; }


        [Display(Name = "Allowances")]

        public string Allowances { get; set; } // Adjusted spelling from Allownaces

        [Required]

        [DataType(DataType.Date)]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "Source Bank")]
        public string SourceBank { get; set; }

        [Required]
        [Display(Name = "Credited By")]
        public string CreditedBy { get; set; }
    }
}
