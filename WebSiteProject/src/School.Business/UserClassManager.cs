using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Business
{
    using Repository;	
	using static Repository.ClassRepositoryMapper;
	using static Repository.UserRepositoryMapper;

	public interface IUserClassManager
	{
		UserClass Add(int userId, int classId);
		bool Remove(int userId, int classId);
		List<UserClass> GetUserClasses(int userId);
		
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


        public UserClass Add(int userId, int classId)
        {
			UserClass newUserClass = new UserClass();
			var addedClass = userClassRepository.Add(userId, classId);
			newUserClass = addedClass.ToBusinessModel();			

			return newUserClass;
		}

        public bool Remove(int userId, int classId)
        {
            throw new NotImplementedException();
        }

		public List<UserClass> GetUserClasses(int userId)
        {
			return userClassRepository.GetUserClasses(userId).Select(t => t.ToBusinessModel()).ToList();				
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
