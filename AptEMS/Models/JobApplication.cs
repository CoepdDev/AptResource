using System;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        // Name: Required, Max Length 100 characters
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        // Email: Required, Email format, Max Length 100 characters
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|in)$", ErrorMessage = "Please enter a valid email address ending with .com or .in.")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MobileNumberValidation("", ErrorMessage = "Invalid mobile number for the selected country.")]
        public string Mobile { get; set; }

        // Father's Name: Optional, Max Length 100 characters
        [StringLength(100, ErrorMessage = "Father's name cannot be longer than 100 characters.")]
        public string FatherName { get; set; }

        // Date of Birth: Required, Must be a valid date
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime Dob { get; set; }

        public string Specialization { get; set; }

        // Total Experience: Optional, Max Length 50 characters
        [StringLength(50, ErrorMessage = "Total experience cannot be longer than 50 characters.")]
        public string TotalExperience { get; set; }


        [Required(ErrorMessage = "Relevant Experience is required.")]
        // Relevant Experience: Optional, Max Length 50 characters
        [StringLength(50, ErrorMessage = "Relevant experience cannot be longer than 50 characters.")]
        public string RelevantExperience { get; set; }

        [Required(ErrorMessage = "Key Skills is required.")]
        // Key Skills: Optional, Max Length 250 characters
        [StringLength(250, ErrorMessage = "Key skills cannot be longer than 250 characters.")]
        public string KeySkills { get; set; }

        [Required(ErrorMessage = "Strengths is required.")]
        // Strengths: Optional, Max Length 250 characters
        [StringLength(250, ErrorMessage = "Strengths cannot be longer than 250 characters.")]
        public string Strengths { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        // Present Designation: Optional, Max Length 100 characters
        [StringLength(100, ErrorMessage = "Designation cannot be longer than 100 characters.")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Current Location is required.")]
        // Company Address: Optional, Max Length 500 characters
        [StringLength(500, ErrorMessage = "Company address cannot be longer than 500 characters.")]
        public string CurrentLocation { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Present CTC must be a valid number.")]
        public string PresentCTC { get; set; }

        [Required(ErrorMessage = "ExpectedCTC is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Expected CTC must be a valid number.")]
        public string ExpectedCTC { get; set; }

        [Required(ErrorMessage = "Notice Period is required.")]
        // Notice Period: Optional, Max Length 50 characters
        [StringLength(50, ErrorMessage = "Notice period cannot be longer than 50 characters.")]
        public string NoticePeriod { get; set; }


        // Resume File Path: Optional, Max Length 255 characters
        [StringLength(255, ErrorMessage = "Resume file path cannot be longer than 255 characters.")]
        public string ResumeFilePath { get; set; }


        [Required]
        public string Education { get; set; }

        [Required]
        public string Gender { get; set; }

        public string CreatedBy { get; set; } = "Portal";
       
    }
}
