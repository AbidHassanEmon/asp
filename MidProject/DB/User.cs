//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MidProject.DB
{
    using MidProject.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Rents = new HashSet<Rent>();
        }
    
        public int User_id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Over18]
        public System.DateTime Dob { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Lisence_no { get; set; }
        [Required]
        public string User_name { get; set; }
        public string Role { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "must be 4 char")]
        [MinLength(4, ErrorMessage = "must be 4 char")]
        public string Password { get; set; }
        public Nullable<int> Otp { get; set; }
        public Nullable<System.DateTime> Otp_expired { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
