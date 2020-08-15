using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Business
{
    using School.Repository;

    public interface IUserClassManager
	{
	    List<UserClass> UserClasss { get; }
	    UserClass UserClass(int id);
	} 

    public class UserClass
	{
	        public int ClassId { get; set; }
	        public int UserId { get; set; }
	        public Class Class { get; set; }
	        public User User { get; set; }
	}

    public class UserClassManager : IUserClassManager
	{
	    private readonly IUserClassRepository userClassRepository;
	
	    public UserClassManager(IUserClassRepository userClassRepository)
	    {
	        this.userClassRepository = userClassRepository;
	    }
	
	
	    public List<UserClass> UserClasss
	    {
	        get
	        {
	            return userClassRepository.UserClasss.Select(t=>t.ToBusinessModel()).ToList();                                               
	        }
	    }
	
	
	    public UserClass UserClass(int id)
	    {
	        return null;
	    } 
	}  

    public static class UserClassRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Repository.UserClass, School.Business.UserClass>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Business.UserClass ToBusinessModel(this School.Repository.UserClass businessObject)
	    {
	        return mapper.Map<School.Business.UserClass>(businessObject);
	    }
	
	
	    public static School.Repository.UserClass ToRepositoryModel(this School.Business.UserClass repositoryObject)
	    {
	        return mapper.Map<School.Repository.UserClass>(repositoryObject);
	    }
	
	}

   

}
