using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptEMS.Models
{
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
