using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Register
    {
        [Required]
        public string UserName { get; set; }
        [Required/*, StringLength()*/]
        public string Password { get; set; }
    }
}