using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class ClientReview
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public string Author { get; set; }
    }
}