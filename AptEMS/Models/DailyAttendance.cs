using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class DailyAttendance
    {

        public int Empid { get; set; }

 
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOJ { get; set; }

        
        public string Designation { get; set; }


        [DataType(DataType.Time)]
        public DateTime Login { get; set; }



        [DataType(DataType.Time)]
        public DateTime Logout { get; set; }





    }
}