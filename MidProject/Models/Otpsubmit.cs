using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidProject.Models
{
    public class Otpsubmit
    {
        [Required]
        public int Otp { get; set; }
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Confirm password does not match")]
        public string NPassword { get; set; }
    }
}