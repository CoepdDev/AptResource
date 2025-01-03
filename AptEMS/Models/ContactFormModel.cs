using System;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        public string CompanyName { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in)$",ErrorMessage = "Please enter a valid email address ending with .com or .in.")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Phone Number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [StringLength(250, ErrorMessage = "Name cannot exceed 250 characters.")]
        public string Designation { get; set; }

        [StringLength(250, ErrorMessage = "Name cannot exceed 250 characters.")]
        public string HiringInterest { get; set; }

        [StringLength(250, ErrorMessage = "Name cannot exceed 250 characters.")]
        public string Requirement { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
