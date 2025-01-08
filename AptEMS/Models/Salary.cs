using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace AptEMS.Models
{
        public class Salary
        {

        [Required(ErrorMessage = " Empid is required")]
        public int Empid { get; set; }

        [Required(ErrorMessage = " Empname is required")]
        public string Empname { get; set; }



        [Required(ErrorMessage = " Credited is required")]
        [DataType(DataType.Date)]
        public DateTime Credited { get; set; }

        [Required(ErrorMessage = " Empsalary is required")]
        public decimal Empsalary { get; set; }

        [Required(ErrorMessage = " Deductions is required")]
        public decimal Deductions { get; set; }


        [Required(ErrorMessage = " Hike is required")]
        public decimal Hike { get; set; }

        [Required(ErrorMessage = " Newsalary is required")]
        public decimal Newsalary { get; set; }

        }
}



