using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ClientReview> ClientReviews { get; set; }
        public IEnumerable<Partnership> Partnerships { get; set; }

        public IEnumerable<Statistic> Statistics { get; set; }

        public List<UploadedImages> UploadedImages { get; set; }
    }
}