using System;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+}{:;'?/>.<,]).{8,}$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; }
    }
}
