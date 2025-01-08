using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AptEMS.ViewModels
{
    public class CombinedJobApplicationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FatherName { get; set; }
        public DateTime Dob { get; set; }
        public string Specialization { get; set; }
        public string TotalExperience { get; set; }
        public string RelevantExperience { get; set; }
        public string KeySkills { get; set; }
        public string Strengths { get; set; }
        public string Designation { get; set; }
        public string CurrentLocation { get; set; }
        public string PresentCTC { get; set; }
        public string ExpectedCTC { get; set; }
        public string NoticePeriod { get; set; }
        public string Education { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public string ResumeFilePath { get; set; }
    }
}
