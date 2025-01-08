using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Rejected
    {
        [Required(ErrorMessage = " this field is required")]
        public string Name { get; set; }



        [Required(ErrorMessage = " this field is required")]
        public string Role { get; set; }



        [Required(ErrorMessage = " this field is required")]
        public string InterviewerName { get; set; }



        [Required(ErrorMessage = " this field is required")]

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }



        [Required(ErrorMessage = " this field is required")]
        public string Remark { get; set; }



        [Required(ErrorMessage = " Phonenumber is required")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "The mobile number must start with a digit between 6 and 9 and contain exactly 10 digits.")]
        //[RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phonenumber must be a valid 10-digit number starting with 6, 7, 8, or 9.")]
        public string Mobile { get; set; }

        [Required]

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Please enter a valid Gmail address.")]
        public string Email { get; set; }
    }
}