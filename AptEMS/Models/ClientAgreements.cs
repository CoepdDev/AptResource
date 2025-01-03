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
        [DataType(DataType.Date)]
        public DateTime AgreementDate { get; set; }

        [Required(ErrorMessage = "Effective Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime EffectiveStartDate { get; set; }

        [StringLength(50, ErrorMessage = "Agreement Duration cannot exceed 50 characters")]
        public string AgreementDuration { get; set; }

        [StringLength(500, ErrorMessage = "Renewal Terms cannot exceed 500 characters")]
        public string RenewalTerms { get; set; }

        [StringLength(500, ErrorMessage = "Termination Conditions cannot exceed 500 characters")]
        public string TerminationConditions { get; set; }

        public string AuthorizedMembers { get; set; }

        [StringLength(200, ErrorMessage = "Company Bank Names cannot exceed 200 characters")]
        public string CompanyBankNames { get; set; }

        public string EmployeeRoles { get; set; }

        public string OfficeTimings { get; set; }

        [StringLength(50, ErrorMessage = "Shift cannot exceed 50 characters")]
        public string Shift { get; set; }

        public string PhoneNumbers { get; set; }

        public string CompanyLogo { get; set; }

        public string WatermarkLogo { get; set; }

        public string LetterHeader { get; set; }

        public string Footer { get; set; }

        public string ServiceType { get; set; }

        [StringLength(2000, ErrorMessage = "Scope of Services cannot exceed 2000 characters")]
        public string ScopeOfServices { get; set; }

        [StringLength(50, ErrorMessage = "Fee Type cannot exceed 50 characters")]
        public string FeeType { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fee Amount must be greater than 0")]
        public decimal FeeAmount { get; set; }

        public string PaymentSchedule { get; set; }

        [StringLength(500, ErrorMessage = "Penalty for Late Payment cannot exceed 500 characters")]
        public string PenaltyForLatePayment { get; set; }

        [StringLength(2000, ErrorMessage = "Client Responsibilities cannot exceed 2000 characters")]
        public string ClientResponsibilities { get; set; }

        [StringLength(2000, ErrorMessage = "Consultancy Responsibilities cannot exceed 2000 characters")]
        public string ConsultancyResponsibilities { get; set; }

        [StringLength(1000, ErrorMessage = "Confidentiality Terms cannot exceed 1000 characters")]
        public string ConfidentialityTerms { get; set; }

        [StringLength(500, ErrorMessage = "Non-Disclosure Agreement terms cannot exceed 500 characters")]
        public string NonDisclosureAgreement { get; set; }

        [StringLength(100, ErrorMessage = "Jurisdiction cannot exceed 100 characters")]
        public string Jurisdiction { get; set; }

        [StringLength(1000, ErrorMessage = "Dispute Resolution Terms cannot exceed 1000 characters")]
        public string DisputeResolutionTerms { get; set; }

        [Required(ErrorMessage = "Client Signatory Name is required")]
        [StringLength(100, ErrorMessage = "Client Signatory Name cannot exceed 100 characters")]
        public string ClientSignatoryName { get; set; }

        [Required(ErrorMessage = "Consultancy Signatory Name is required")]
        [StringLength(100, ErrorMessage = "Consultancy Signatory Name cannot exceed 100 characters")]
        public string ConsultancySignatoryName { get; set; }

        [Required(ErrorMessage = "Signature Date is required")]
        [DataType(DataType.Date)]
        public DateTime SignatureDate { get; set; }

        public string DigitalSignature { get; set; }

        [StringLength(2000, ErrorMessage = "Additional Notes cannot exceed 2000 characters")]
        public string AdditionalNotes { get; set; }
    }
}
