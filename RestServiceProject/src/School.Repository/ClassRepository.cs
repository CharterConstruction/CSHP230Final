

using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository
{
    public interface IClassRepository
	{
	    List<Class> Classes { get; }
	    Class Class(int id);
	}

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

    public class ClassRepository : IClassRepository
	{
	    public List<Class> Classes
	    {
	        get
	        {
	            return DatabaseAccessor.Instance.Class.Select(t=>t.ToRepositoryModel()).ToList();                                               
	        }
	    }
	
	
	    public Class Class(int id)
	    {
	        return DatabaseAccessor.Instance.Class.FirstOrDefault(t => t.ClassId == id).ToRepositoryModel();
		} 
	}

    public static class ClassRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => 
			cfg.CreateMap<School.Database.Models.Class, School.Repository.Class>()
			.ForMember(dest => dest.UserClass, opt => opt.Ignore())
			.ReverseMap());


	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Repository.Class ToRepositoryModel(this School.Database.Models.Class businessObject)
	    {
	        return mapper.Map<School.Repository.Class>(businessObject);
	    }
	
	
	    public static School.Database.Models.Class ToDbModel(this School.Repository.Class repositoryObject)
	    {
	        return mapper.Map<School.Database.Models.Class>(repositoryObject);
	    }
	
	}

}
