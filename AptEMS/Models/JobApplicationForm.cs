using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    
    public class JobApplicationForm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MobileNumberValidation("", ErrorMessage = "Invalid mobile number for the selected country.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please select your gender.")]
        [StringLength(10, ErrorMessage = "Gender must be no more than 10 characters.")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Father's name is required.")]
        [StringLength(100, ErrorMessage = "Father's name cannot exceed 100 characters.")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Column(TypeName = "date")] // Maps to SQL Server's DATE type
        public DateTime Dob { get; set; }



        [Required(ErrorMessage = "Specialization is required.")]
        [StringLength(150, ErrorMessage = "Specialization cannot exceed 150 characters.")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Total experience is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Total experience must be a valid number.")]
        public string TotalExperience { get; set; }

        [Required(ErrorMessage = "Relevant experience is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Relevant experience must be a valid number.")]
        public string RelevantExperience { get; set; }

        [Required(ErrorMessage = "Key skills are required.")]
        [StringLength(200, ErrorMessage = "Key skills cannot exceed 200 characters.")]
        public string KeySkills { get; set; }

        [StringLength(200, ErrorMessage = "Strengths cannot exceed 200 characters.")]
        public string Strengths { get; set; }

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(100, ErrorMessage = "Designation cannot exceed 100 characters.")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Current location is required.")]
        [StringLength(100, ErrorMessage = "Current location cannot exceed 100 characters.")]
        public string CurrentLocation { get; set; }

        [Required(ErrorMessage = "Present CTC is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Present CTC must be a valid number.")]
        public string PresentCTC { get; set; }

        [Required(ErrorMessage = "Expected CTC is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Expected CTC must be a valid number.")]
        public string ExpectedCTC { get; set; }

        [Required(ErrorMessage = "Please select a notice period.")]
        public string NoticePeriod { get; set; }

        public string ResumeFilePath { get; set; }

        [Required(ErrorMessage = "Qualification is required.")]
        public string Education { get; set; }

        public string CreatedBy { get; set; } = "Associate";
    }
}
