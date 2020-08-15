using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Business
{
    using School.Repository;

    public interface IUserManager
	{
		User LogIn(string email, string password);
		User Register(string email, string password); 
		
		List<User> Users { get; }
	    User User(int id);
	} 

    public class User
	{
		public User()
		{
			UserClass = new HashSet<UserClass>();
		}

		public int UserId { get; set; }
		public string UserEmail { get; set; }
		//public string UserPassword { get; set; }
		public bool UserIsAdmin { get; set; }

		public ICollection<UserClass> UserClass { get; set; }
	}

    public class UserManager : IUserManager
	{
	    private readonly IUserRepository userRepository;
	
	    public UserManager(IUserRepository userRepository)
	    {
	        this.userRepository = userRepository;
	    }

		public User LogIn(string email, string password)
		{
			var user = userRepository.LogIn(email, password);

			if (user == null)
			{
				return null;
			}

			return new User { UserId = user.UserId, UserEmail = user.UserEmail };
		}

		public User Register(string email, string password)
		{
			var user = userRepository.Register(email, password);

			if (user == null)
			{
				return null;
			}

			return new User { UserId = user.UserId, UserEmail = user.UserEmail };
		}
		public List<User> Users
	    {
	        get
	        {
	            return userRepository.Users.Select(t=>t.ToBusinessModel()).ToList();                                               
	        }
	    }
	
	
	    public User User(int id)
	    {
	        return null;
	    } 
	}  

    public static class UserRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Repository.User, School.Business.User>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Business.User ToBusinessModel(this School.Repository.User businessObject)
	    {
	        return mapper.Map<School.Business.User>(businessObject);
	    }
	
	
	    public static School.Repository.User ToRepositoryModel(this School.Business.User repositoryObject)
	    {
	        return mapper.Map<School.Repository.User>(repositoryObject);
	    }
	
	}

   

}
