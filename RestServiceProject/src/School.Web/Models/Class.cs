using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Web.Models
{    
    public class Class
	{
		public Class()
		{
			UserClass = new HashSet<UserClass>();
		}

		public int ClassId { get; set; }
		public string ClassName { get; set; }
		public string ClassDescription { get; set; }
		public decimal ClassPrice { get; set; }

		public ICollection<UserClass> UserClass { get; set; }
	}

    public static class ClassWebMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Business.Class, School.Web.Models.Class>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Web.Models.Class ToWebModel(this School.Business.Class businessObject)
	    {
	        return mapper.Map<School.Web.Models.Class>(businessObject);
	    }
	
	
	    public static School.Business.Class ToBusinessModel(this School.Web.Models.Class repositoryObject)
	    {
	        return mapper.Map<School.Business.Class>(repositoryObject);
	    }
	
	}
}
