using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class SalaryComparison
    {
        [Required(ErrorMessage = " this field is required")]
        public int Empid { get; set; }


        [Required(ErrorMessage = " this field is required")]
        public string Empname { get; set; }


        [Required(ErrorMessage = " this field is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public int NetSalary { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public int PreviousMonthSalary { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public int ThisMonthSalary { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public int SalaryChange { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public string Reason { get; set; }

        [Required(ErrorMessage = " this field is required")]
        public int Handledby { get; set; }



    }
}