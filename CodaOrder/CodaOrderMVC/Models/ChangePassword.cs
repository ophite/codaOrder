using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ChangePassword
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string PasswordOld { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string PasswordNew { get; set; }
    }
}