using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }

 
}
