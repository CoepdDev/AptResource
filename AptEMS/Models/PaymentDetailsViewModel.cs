using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class PaymentDetailsViewModel
    {
        public int AgreementID { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal AmountPaid { get; set; }

        public string PaymentStatus { get; set; }
        public string ContactNumber { get; set; }
       
        public string PaymentMethod { get; set; }

        public DateTime? PaymentDoneDate { get; set; }

        
    }

}