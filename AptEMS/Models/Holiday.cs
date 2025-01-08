using AptEMS.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Holiday
    {
        public int HolidayId { get; set; }

        [Required]
        public string HolidayName { get; set; }

        [Required]
        [DataType(DataType.Date)]

        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomExpiryDate("FromDate")] 
        public DateTime ToDate { get; set; }
        public int Days { get; set; }
        public DateTime CreatedAt { get; set; }

        
    }

}