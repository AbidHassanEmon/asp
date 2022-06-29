using MidProject.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidProject.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string message = String.Format("The {0} field is invalid.", validationContext.DisplayName ?? validationContext.MemberName);

            if (value == null)
                return new ValidationResult(message);

            var db = new Project_DBEntities();
            var users = db.Users.ToList();
            foreach (var p in users)
            {
                if (value.ToString().Equals(p.Email))
                {
                    return new ValidationResult("Email Already Exists!!!Must be Unique");
                }
            }

            return ValidationResult.Success;

        }
    }
}