using System;
using System.Collections.Generic;
using System.Linq;

namespace School.API.Models
{


	using static Models.ClassWebMapper;
	using static Models.UserWebMapper;

	public class UserClass
	{
	        public int ClassId { get; set; }
	        public int UserId { get; set; }
	        public Class Class { get; set; }
	        public User User { get; set; }
	}

    public static class UserClassWebMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Business.UserClass, School.API.Models.UserClass>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.API.Models.UserClass ToWebModel(this School.Business.UserClass businessObject)
	    {
			var targetClass = businessObject.Class.ToWebModel();
			var targetUser = businessObject.User.ToWebModel();


			return new School.API.Models.UserClass()
			{
				ClassId = businessObject.ClassId,
				UserId = targetUser.UserId,
				Class = targetClass,
				User = targetUser
			};
	    }
	
	
	    public static School.Business.UserClass ToBusinessModel(this School.API.Models.UserClass repositoryObject)
	    {
	        return mapper.Map<School.Business.UserClass>(repositoryObject);
	    }
	
	}
}
