using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
		[StringLength(25, MinimumLength = 4, ErrorMessage = "Password must be between 4-25 characters in length")]
		public string Password { get; set; }

		[Required]
		[Display(Name = "First Name")]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "First Name must be between 2-25 characters in length")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "Last Name must be between 2-25 characters in length")]
		public string LastName { get; set; }






	}


}
