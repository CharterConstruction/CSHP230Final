using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace School.API.Models
{    
    public class User
	{
		public User()
		{
			UserClass = new HashSet<UserClass>();
		}

		[Required]
		public int UserId { get; set; }
		
		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string UserEmail { get; set; }


		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[StringLength(25, MinimumLength = 4, ErrorMessage = "Password must be between 4-25 characters in length")]
		public string UserPassword { get; set; }

		public bool UserIsAdmin { get; set; }

		[Required]		
		[Display(Name = "First Name")]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "First Name must be between 2-25 characters in length")]
		public string FirstName { get; set; }
		
		[Required]
		[Display(Name = "Last Name")]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "Last Name must be between 2-25 characters in length")]
		public string LastName { get; set; }

		public string Name => String.Format("{0} {1}",FirstName,LastName);

		public ICollection<UserClass> UserClass { get; set; }
	}

    public static class UserWebMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Business.User, School.API.Models.User>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.API.Models.User ToWebModel(this School.Business.User businessObject)
	    {
	        return mapper.Map<School.API.Models.User>(businessObject);
	    }
	
	
	    public static School.Business.User ToBusinessModel(this School.API.Models.User repositoryObject)
	    {
	        return mapper.Map<School.Business.User>(repositoryObject);
	    }	
	}




	public static class RegexUtilities
	{
		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				// Normalize the domain
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									  RegexOptions.None, TimeSpan.FromMilliseconds(200));

				// Examines the domain part of the email and normalizes it.
				string DomainMapper(Match match)
				{
					// Use IdnMapping class to convert Unicode domain names.
					var idn = new IdnMapping();

					// Pull out and process domain name (throws ArgumentException on invalid)
					var domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException e)
			{
				return false;
			}
			catch (ArgumentException e)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}
	}










}
