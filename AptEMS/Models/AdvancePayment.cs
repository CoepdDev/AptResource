using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class AdvancePayment
    {

        [Required(ErrorMessage = " this field is required")]
        public int Empid { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public string Empname { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public string AdvanceType { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public decimal AdvanceAmount { get; set; }

        [Required(ErrorMessage = " this field is required")]

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = " this field is required")]

        [DataType(DataType.Date)]
        public DateTime ApprovalDate { get; set; }
        [Required(ErrorMessage = " this field is required")]
        public int RepaymentMonths { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public string RepaymentSchedule { get; set; }


        [Required(ErrorMessage = " this field is required")]
        public decimal RepaymentAmount { get; set; }

        [Required(ErrorMessage = " this field is required")]

        [DataType(DataType.Date)]
        public DateTime DeductionStartDate { get; set; }


        [Required(ErrorMessage = " this field is required")]
        [DataType(DataType.Date)]
        public DateTime DeductionEndDate { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public string Remarks { get; set; }








    }
}