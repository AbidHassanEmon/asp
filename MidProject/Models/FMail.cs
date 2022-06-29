using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidProject.Models
{
    public class FMail
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
    }
}