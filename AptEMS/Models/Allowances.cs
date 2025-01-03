using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Allowances
    {

        [Required(ErrorMessage = " Empid is required")]
        public int Empid { get; set; }

        [Required(ErrorMessage = " Empname is required")]
        public string Empname { get; set; }

        [Required(ErrorMessage = " Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = " AllowanceType is required")]
        public int AllowanceType { get; set; }

        [Required(ErrorMessage = " AllowanceAmount is required")]
        public int AllowanceAmount { get; set; }

        [Required(ErrorMessage = " Frequency is required")]
        public int Frequency { get; set; }

        [Required(ErrorMessage = " EffectiveDate is required")]

        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = " EndDate is required")]

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = " Reason is required")]
        public string Reason { get; set; }

        [Required(ErrorMessage = " Status is required")]
        public string Status { get; set; }

    }
}