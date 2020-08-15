

using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository
{
    public interface IUserClassRepository
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

    public class UserClassRepository : IUserClassRepository
	{
	    public List<UserClass> UserClasss
	    {
	        get
	        {
	            return DatabaseAccessor.Instance.UserClass.Select(t=>t.ToRepositoryModel()).ToList();                                               
	        }
	    }
	
	
	    public UserClass UserClass(int id)
	    {
	        return null;
	    } 
	}

    public static class UserClassRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Database.Models.UserClass, School.Repository.UserClass>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Repository.UserClass ToRepositoryModel(this School.Database.Models.UserClass businessObject)
	    {
	        return mapper.Map<School.Repository.UserClass>(businessObject);
	    }
	
	
	    public static School.Database.Models.UserClass ToDbModel(this School.Repository.UserClass repositoryObject)
	    {
	        return mapper.Map<School.Database.Models.UserClass>(repositoryObject);
	    }
	
	}

}
