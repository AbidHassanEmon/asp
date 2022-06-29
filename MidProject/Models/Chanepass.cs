using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidProject.Models
{
    public class Chanepass
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Npassword { get; set; }
        [Required]
        public string NNPassword { get; set; }
    }
}