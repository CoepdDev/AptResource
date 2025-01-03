using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Shiftreg
    {
        [Required(ErrorMessage = " Shift Type  is required")]
        public string s_type { get; set; }


        [Required(ErrorMessage = " Shift Start is required")]
        [DataType(DataType.Time)]
        public DateTime? s_start { get; set; }


        [Required(ErrorMessage = " Shift End is required")]
        [DataType(DataType.Time)]
        public DateTime? s_end { get; set; }


        [DataType(DataType.Time),Required(ErrorMessage = " Break is required")]
        public DateTime? brk { get; set; }


        [Required(ErrorMessage = " Attendence is required")]
        public string attendence { get; set; }


    }
}


//namespace AptEMS.Models
//{
//    public class Employee
//    {
//        /* 1*/
//        public int Id { get; set; }

//        /* 2*/
//        public string FirstName { get; set; } = string.Empty;
//        /* 3*/
//        public string LastName { get; set; } = string.Empty;
//        /* 4*/
//        public string Permanentadress { get; set; } = string.Empty;
//        /* 5*/
//        public string Currentadress { get; set; } = string.Empty;
//        /* 6*/
//        public string City { get; set; } = string.Empty;
//        /* 7*/
//        public string State { get; set; } = string.Empty;
//        /* 8*/
//        public int PostaZipCode { get; set; }
//        /* 9*/
//        public string Country { get; set; } = string.Empty;
//        /* 10*/
//        public int Phonenumber { get; set; }
//        /* 11*/
//        public string PersonalemailID { get; set; } = string.Empty;
//        /* 12*/
//        public string OfficeemailID { get; set; } = string.Empty;
//        /* 13*/
//        public string Bankname { get; set; } = string.Empty;
//        /* 14*/
//        public int Bankaccountnumber { get; set; }
//        /* 15*/
//        public string BankIFSCcode { get; set; } = string.Empty;

//        [DataType(DataType.Date)]
//        /* 16*/
//        public DateTime DOJ { get; set; }
//        /* 17*/
//        public string Branch { get; set; } = string.Empty;
//        /* 18*/
//        public string Designation { get; set; } = string.Empty;
//        /* 19*/
//        public string Summary { get; set; } = string.Empty;
//    }
//}