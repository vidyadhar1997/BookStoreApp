using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class UserRegistration
    {
     /*   [Required]
        public int UserId { get; set; }*/
        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]{2,15}$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]{2,15}$", ErrorMessage = "Invalid First Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{1,}([.]?[-]?[+]?[a-zA-Z0-9]{1,})?[@]{1}[a-zA-Z0-9]{1,}[.]{1}[a-z]{2,3}([.]?[a-z]{2})?$", ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*[@#$&*_+-]{1}[a-zA-Z0-9]*$", ErrorMessage = "Invalid Password type")]
        public string Password { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }
    }
}
