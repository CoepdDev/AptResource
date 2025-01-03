using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class AttendanceConfigure
    {
        [Display(Name = "Created By")]
        [Required]
        public string CreatedBy { get; set; }







        [Display(Name = "Month Name")]
      
        public string MonthName { get; set; }





        [Display(Name = "Working Days")]
       
        public string WorkingDays { get; set; }





        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]

        public DateTime CreatedDate { get; set; }




    }
}