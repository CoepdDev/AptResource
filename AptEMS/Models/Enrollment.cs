using System;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in)$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Phone Number must be 10 digits.")]
        public string Mobile { get; set; }

        
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string Company { get; set; }

        
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; }

        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters.")]
        public string Remarks { get; set; }

        [Required]
        public int PackageId { get; set; } // Link to PricingPackage
    }
}
