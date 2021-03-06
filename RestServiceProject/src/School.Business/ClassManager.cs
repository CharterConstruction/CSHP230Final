using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Business
{
    using School.Repository;

    public interface IClassManager
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

    public class ClassManager : IClassManager
	{
	    private readonly IClassRepository classRepository;
	
	    public ClassManager(IClassRepository classRepository)
	    {
	        this.classRepository = classRepository;
	    }
	
	
	    public List<Class> Classes
	    {
	        get
	        {
	            return classRepository.Classes.Select(t=>t.ToBusinessModel()).ToList();                                               
	        }
	    }
	
	
	    public Class Class(int id)
	    {
	        return null;
	    } 
	}  

    public static class ClassManagerMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Repository.Class, School.Business.Class>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Business.Class ToBusinessModel(this School.Repository.Class businessObject)
	    {
	        return mapper.Map<School.Business.Class>(businessObject);
	    }
	
	
	    public static School.Repository.Class ToRepositoryModel(this School.Business.Class repositoryObject)
	    {
	        return mapper.Map<School.Repository.Class>(repositoryObject);
	    }
	
	}

   

}
