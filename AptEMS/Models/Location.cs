using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public bool IsActive { get; set; }
    }

}