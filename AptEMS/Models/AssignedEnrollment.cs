using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class AssignedEnrollment
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public string AssignedEmployee { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}

