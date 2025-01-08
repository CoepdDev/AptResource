using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Incentives
    {

        
        [Required(ErrorMessage = " Empid is required")]
        public int Empid { get; set; }


        [Required(ErrorMessage = " Empname is required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = " Designation is required")]
        public string Designation { get; set; }


        [Required(ErrorMessage = " IncentiveType is required")]
        public string IncentiveType { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Loan Sanction Amount must be a valid number.")]
        [Required(ErrorMessage = " Amount is required")]
        public string Amount { get; set; }

        [Required(ErrorMessage = " DateAwared is required")]
        [DataType(DataType.Date)]
        public DateTime DateAwared { get; set; }


        [Required(ErrorMessage = " Status is required")]
        public string Status { get; set; }


        [Required(ErrorMessage = " PaymentDate is required")]

        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }


        [Required(ErrorMessage = " Manager is required")]
        public string Manager { get; set; }



        [Required(ErrorMessage = " Approval is required")]
        public string Approval { get; set; }

    }
}