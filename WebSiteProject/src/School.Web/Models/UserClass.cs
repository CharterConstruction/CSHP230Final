using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Web.Models
{    
    public class UserClass
	{
	        public int ClassId { get; set; }
	        public int UserId { get; set; }
	        public Class Class { get; set; }
	        public User User { get; set; }
	}

    public static class UserClassWebMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Business.UserClass, School.Web.Models.UserClass>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Web.Models.UserClass ToWebModel(this School.Business.UserClass businessObject)
	    {
	        return mapper.Map<School.Web.Models.UserClass>(businessObject);
	    }
	
	
	    public static School.Business.UserClass ToBusinessModel(this School.Web.Models.UserClass repositoryObject)
	    {
	        return mapper.Map<School.Business.UserClass>(repositoryObject);
	    }
	
	}
}
