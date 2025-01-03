using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Employee
    {
        [Required(ErrorMessage = " ID is required")]
        public int Id { get; set; }


        [Required(ErrorMessage = " FirstName is required")]
        public string FirstName { get; set; } 


        [Required(ErrorMessage = " LastName is required")]
        public string LastName { get; set; } 



        [Required(ErrorMessage = " Permanent Address is required")]
        public string PermanentAddress { get; set; } 


        [Required(ErrorMessage = " Current Address is required")]
        public string CurrentAddress { get; set; } 


        [Required(ErrorMessage = " City is required")]

        public string City { get; set; } 


        [Required(ErrorMessage = " State is required")]
        public string State { get; set; }




   



        [Display(Name = "Nationality")]
        public string Country { get; set; } 


        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "The mobile number must start with a digit between 6 and 9 and contain exactly 10 digits.")]
        [Required(ErrorMessage = " Phonenumber is required")]
        public string PhoneNumber { get; set; }


        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "The mobile number must start with a digit between 6 and 9 and contain exactly 10 digits.")]
        public String PostalZipCode { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.(com|in)$", ErrorMessage = "Please enter a valid Gmail address.")]
        [Required(ErrorMessage = " PersonalemailID is required")]
        public string PersonalEmailID { get; set; } 


        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Please enter a valid Gmail address.")]
      
        public string OfficeEmailID { get; set; } 



        public string BankName { get; set; } 


 
        public string BankAccountNumber { get; set; }





        public string BankIFSCCode { get; set; } 




        [Required(ErrorMessage = " DOJ is required")]

        [DataType(DataType.Date)]
         public DateTime DOJ { get; set; }



        public string Branch { get; set; } 


        [Required(ErrorMessage = " Designation is required")]
        public string Designation { get; set; } 



     public string Summary { get; set; }




        [Required]

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }


        public string Empphoto { get; set; } 



        [Required(ErrorMessage = " PANno is required")]
        [RegularExpression(@"^[A-Z]{5}\d{4}[A-Z]{1}$", ErrorMessage = "Please Enter Valid PAN (e.g., ABCDE1234F).")]
        public string PANno { get; set; }



        public string PANphoto { get; set; }



        [Required(ErrorMessage = " Aadharno is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar number must be exactly 12 digits.")]
        public string Aadharno { get; set; }



        public string Aadharphoto { get; set; }


        public bool isactive { get; set; }




    }
}