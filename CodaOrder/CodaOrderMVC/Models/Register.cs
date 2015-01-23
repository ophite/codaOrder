using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Register
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string Password { get; set; }
        [Required]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password must be equal")]
        public string PasswordConfirm { get; set; }
    }
}