//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AptEMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Deduction
    {

        public int ID { get; set; }

        [Required(ErrorMessage = " This Field is required")]

        public string EmpName { get; set; }
        [Required(ErrorMessage = " FirstName is required")]

        public string Designation { get; set; }
        [Required(ErrorMessage = "  This Field is required")]
        public string DeductionType { get; set; }
        [Required(ErrorMessage = " This Field is required")]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        [Required(ErrorMessage = " This Field is required")]

        public Nullable<int> DeductionAmount { get; set; }
        [Required(ErrorMessage = "  This Field is required")]

        public string Reason { get; set; }
        [Required(ErrorMessage = "  This Field is required")]
        public Nullable<int> DeductionFrequency { get; set; }
        [Required(ErrorMessage = "  This Field is required")]
        public string ApprovalStatus { get; set; }

        [Required(ErrorMessage = "  This Field is required")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ApprovalDate { get; set; }
    }
}
