using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class RestorePassword
    {
        [Required]
        public string Email { get; set; }
    }

    public class RestorePasswordParam
    {
        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string Password { get; set; }
        public string token { get; set; }
    }
}