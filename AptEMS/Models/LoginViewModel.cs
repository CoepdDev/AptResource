using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace AptEMS.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
