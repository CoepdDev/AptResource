using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class JobApplicationViewModel
    {
      /*  public JobPosting JobPosting { get; set; }*/

        public JobApplication JobApplication { get; set; }

        public JobPost JobPost { get; set; }


    }
}
