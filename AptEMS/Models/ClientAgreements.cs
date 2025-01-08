using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  
using System.Linq;
using System.Web;


namespace AptEMS.Models
{
    public class ClientAgreements
    {
        [Key]
        public int AgreementID { get; set; }

        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(100, ErrorMessage = "Client Name cannot exceed 100 characters")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Phone(ErrorMessage = "Invalid Contact Number")]
        [StringLength(15, ErrorMessage = "Contact Number cannot exceed 15 digits")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Company Address is required")]
        [StringLength(200, ErrorMessage = "Company Address cannot exceed 200 characters")]
        public string CompanyAddress { get; set; }

        [Url(ErrorMessage = "Invalid Website URL")]
        public string WebsiteURL { get; set; }

        [Required(ErrorMessage = "Agreement Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? AgreementDate { get; set; }

        public string AgreementDateFormatted => AgreementDate?.ToString("yyyy-MM-dd");



        [Required(ErrorMessage = "Effective Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveStartDate { get; set; }

        [StringLength(50, ErrorMessage = "Agreement Duration cannot exceed 50 characters")]
        public string AgreementDuration { get; set; }

        
        public string AuthorizedMembers { get; set; }

        [StringLength(200, ErrorMessage = "Company Bank Names cannot exceed 200 characters")]
        public string CompanyBankNames { get; set; }

        public string EmployeeRoles { get; set; }

        public string OfficeTimings { get; set; }

        [StringLength(50, ErrorMessage = "Shift cannot exceed 50 characters")]
        public string Shift { get; set; }


        [Required(ErrorMessage = "Mobile number is required.")]
        [MobileNumberValidation("", ErrorMessage = "Invalid mobile number for the selected country.")]
        public string PhoneNumbers { get; set; }

        public string CompanyLogo { get; set; }

        public string WatermarkLogo { get; set; }

        public string LetterHeader { get; set; }

        public string Footer { get; set; }

       

        [Range(0.01, double.MaxValue, ErrorMessage = "Fee Amount must be greater than 0")]
        public decimal FeeAmount { get; set; }

        public string DigitalSignature { get; set; }

        [StringLength(2000, ErrorMessage = "Additional Notes cannot exceed 2000 characters")]
        public string AdditionalNotes { get; set; }

        public bool IsPaid { get; set; }

        public bool IsDeleted { get; set; }
    }
}
