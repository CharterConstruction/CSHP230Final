using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Web.Models
{    
    public class User
	{
		public User()
		{
			UserClass = new HashSet<UserClass>();
		}

		public int UserId { get; set; }
		public string UserEmail { get; set; }
		public string UserPassword { get; set; }
		public bool UserIsAdmin { get; set; }

		public ICollection<UserClass> UserClass { get; set; }
	}

    public static class UserWebMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Business.User, School.Web.Models.User>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Web.Models.User ToWebModel(this School.Business.User businessObject)
	    {
	        return mapper.Map<School.Web.Models.User>(businessObject);
	    }
	
	
	    public static School.Business.User ToBusinessModel(this School.Web.Models.User repositoryObject)
	    {
	        return mapper.Map<School.Business.User>(repositoryObject);
	    }
	
	}
}
