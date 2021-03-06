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
		private readonly IUserRepository userRepository;
		private readonly IClassRepository classRepository;


		public UserClassManager(IUserClassRepository userClassRepository, IUserRepository userRepository, IClassRepository classRepository)
		{																  
	        this.userClassRepository = userClassRepository;
			this.userRepository = userRepository;
			this.classRepository = classRepository;
		}


        public UserClass Add(int userId, int classId)
        {
			UserClass newUserClass = new UserClass();

			var addedClass = userClassRepository.Add(userId, classId);

			newUserClass = addedClass.ToBusinessModel(userRepository, classRepository);

			return newUserClass;
		}

        public bool Remove(int userId, int classId)
        {
			return userClassRepository.Remove(userId, classId);

		}

		public List<UserClass> GetUserClasses(int userId)
        {

			var userClasses = userClassRepository.GetUserClasses(userId)
				.Select(t =>
				{
					var targetUser = userRepository.User(userId).ToBusinessModel();
					var targetClass = classRepository.Class(t.ClassId).ToBusinessModel();

					return new UserClass()
					{
						ClassId = t.ClassId,
						UserId = t.UserId,
						Class = targetClass,
						User = targetUser
					};
				})
				.ToList();

			return userClasses;
		}


	}  

    public static class UserClassRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Repository.UserClass, School.Business.UserClass>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();

		
		public static School.Business.UserClass ToBusinessModel(this School.Repository.UserClass repositoryObject, IUserRepository userRepository, IClassRepository classRepository)
	    {
			var targetUser = userRepository.User(repositoryObject.UserId).ToBusinessModel();
			var targetClass = classRepository.Class(repositoryObject.ClassId).ToBusinessModel();

			return new UserClass()
			{
				ClassId = repositoryObject.ClassId,
				UserId = repositoryObject.UserId,
				Class = targetClass,
				User = targetUser
			};
	    }
	
	
	    public static School.Repository.UserClass ToRepositoryModel(this School.Business.UserClass repositoryObject)
	    {
	        return mapper.Map<School.Repository.UserClass>(repositoryObject);
	    }
	
	}

   

}
