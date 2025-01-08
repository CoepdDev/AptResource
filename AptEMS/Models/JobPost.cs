using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptEMS.Attributes;

namespace AptEMS.Models
{
    public class JobPost
    {
        public int JobPostID { get; set; }
        // JobType validation
        [Required(ErrorMessage = "Job Type is required.")]
        public string JobType { get; set; }

        // EmploymentType validation
        [Required(ErrorMessage = "Employment Type is required.")]
        public string EmploymentType { get; set; }

        
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        
        public DateTime? ScheduledDate { get; set; }


        [Required(ErrorMessage = "Expiry Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [Display(Name = "Expiry Date")]
        [CustomExpiryDate(ErrorMessage = "Expiry date must be in the future.")]
        public DateTime ExpiryDate { get; set; }


        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(100, ErrorMessage = "Job Title cannot be longer than 100 characters.")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }


        // MinExperience and MaxExperience validation
        [Range(0, int.MaxValue, ErrorMessage = "Experience must be a positive number.")]
        public int MinExperience { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Experience must be a positive number.")]
        public int MaxExperience { get; set; }

        // Skills validation
        [StringLength(500, ErrorMessage = "Skills cannot be longer than 500 characters.")]
        public string Skills { get; set; }

        // JobDescription validation
        [AllowHtml]
        [Required(ErrorMessage = "Job Description is required.")]
        public string JobDescription { get; set; }

        // JobLocation validation
        [Required(ErrorMessage = "Job Location is required.")]
        [StringLength(200, ErrorMessage = "Job Location cannot be longer than 200 characters.")]
        public string JobLocation { get; set; }

        // Salary validation
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal MinSalary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal MaxSalary { get; set; }

        // Industry validation
        [StringLength(200, ErrorMessage = "Industry name cannot be longer than 200 characters.")]
        public string Industry { get; set; }

        // FunctionsAndRoles validation
        [StringLength(1000, ErrorMessage = "Functions and Roles cannot be longer than 1000 characters.")]
        public string FunctionsAndRoles { get; set; }

        // Education validation
        [StringLength(500, ErrorMessage = "Education details cannot be longer than 500 characters.")]
        public string Education { get; set; }

        // RecruiterName validation
        [Required(ErrorMessage = "Recruiter Name is required.")]
        [StringLength(200, ErrorMessage = "Recruiter Name cannot be longer than 200 characters.")]
        public string RecruiterName { get; set; }

        // RecruiterDesignation validation
        [StringLength(200, ErrorMessage = "Recruiter Designation cannot be longer than 200 characters.")]
        public string RecruiterDesignation { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MobileNumberValidation("", ErrorMessage = "Invalid mobile number for the selected country.")]
        public string RecruiterPhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.(com|in)$", ErrorMessage = "Please enter a valid Gmail address.")]
        [Required(ErrorMessage = " Recruiter email ID is required")]
        public string RecruiterEmail { get; set; }

        // RecruiterCompanyName validation
        [StringLength(200, ErrorMessage = "Recruiter Company Name cannot be longer than 200 characters.")]
        public string RecruiterCompanyName { get; set; }

        // RecruiterAboutCompany validation
        [StringLength(1000, ErrorMessage = "Recruiter About Company cannot be longer than 1000 characters.")]
        public string RecruiterAboutCompany { get; set; }

        public String Createdby { get; set; }

        [DataType(DataType.Date)]
        public DateTime Createdon { get; set; }

        public bool IsActive { get; set; }
    }
}