using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Hike
    {

        [Required]
        public int Empid { get; set; }

        [Required]

        [Display(Name = "Employee Name")]
        public string FirstName { get; set; }

        [Required]
        public string Designation { get; set; }


        public string Position { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Dateofstart { get; set; }

        [Required]
        public float Basesalary { get; set; }

        [Required]
        public float Hikee { get; set; }

        [Required]
        public string Reporter { get; set; }

        [Required]
        public string Approver { get; set; }

        public string Department { get; set; }
    }
}