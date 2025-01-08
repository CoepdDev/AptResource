using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class LOP
    {
        [Required(ErrorMessage = " Empid is required")]
        public int Empid { get; set; }



        [Required(ErrorMessage = " Empname is required")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = " Designation is required")]
        public string Designation { get; set; }




        [Required(ErrorMessage = " LeaveType is required")]
        public string LeaveType { get; set; }



        [Required(ErrorMessage = " LeaveStart is required")]
        [DataType(DataType.Date)]
        public DateTime LeaveStart { get; set; }



        [Required(ErrorMessage = " LeaveEnd is required")]
        [DataType(DataType.Date)]
        public DateTime LeaveEnd { get; set; }



        [Required(ErrorMessage = " TotalLeaveDays is required")]
        public string TotalLeaveDays { get; set; }



        [Required(ErrorMessage = " Reason is required")]
        public string Reason { get; set; }



        [Required(ErrorMessage = " LeaveStatus is required")]
        public string LeaveStatus { get; set; }



        [Required(ErrorMessage = " LeaveLeft is required")]
        public string LeaveLeft { get; set; }


    }
}