

using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository
{
    public interface IUserRepository
	{
		User LogIn(string email, string password);
		User Register(string firstName, string LastName, string email, string password);
		List<User> Users { get; }
	    User User(int id);
		bool Remove(int id);
		bool Update(User user);

	}

    public class User
	{
		public User()
		{
			UserClass = new HashSet<UserClass>();
		}

		public int UserId { get; set; }
		public string UserEmail { get; set; }
		public string UserPassword { get; set; }
		public bool UserIsAdmin { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? CreateDate { get; set; }

		public ICollection<UserClass> UserClass { get; set; }
	}

    public class UserRepository : IUserRepository
	{
	    public List<User> Users
	    {
	        get
	        {
	            return DatabaseAccessor.Instance.User.Select(t=>t.ToRepositoryModel()).ToList();                                               
	        }
	    }
	
	
	    public User User(int id)
	    {
			return DatabaseAccessor.Instance.User.FirstOrDefault(t => t.UserId == id).ToRepositoryModel();
		}

		public User	LogIn(string email, string password)
		{
			var user = DatabaseAccessor.Instance.User
				.FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
									  && t.UserPassword == password);

			if (user == null)
			{
				return null;
			}

			return new User
			{
				UserId = user.UserId,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserEmail = user.UserEmail,
				CreateDate = user.CreateDate
			};
		}

		public User	Register(string firstName, string LastName, string email, string password)
		{
			var user = DatabaseAccessor.Instance.User
					.Add(new School.Database.Models.User()
					{
						FirstName = firstName,
						LastName = LastName,
						UserEmail = email,
						UserPassword = password,
						CreateDate = DateTime.Now
					});

			DatabaseAccessor.Instance.SaveChanges();

			return new User { 
				UserId = user.Entity.UserId,
				FirstName = user.Entity.FirstName,
				LastName = user.Entity.LastName,
				UserEmail = user.Entity.UserEmail,
				CreateDate = user.Entity.CreateDate
			};
		}



		public bool Remove(int userId)
		{
			var itemsToRemove = DatabaseAccessor.Instance.User
					.Where(t => t.UserId == userId);

			if (itemsToRemove.Count() == 0)
			{
				return false;
			}

			DatabaseAccessor.Instance.User.Remove(itemsToRemove.First());

			DatabaseAccessor.Instance.SaveChanges();

			return true;

		}


		public bool Update(User updatedUser)
        {
			var userToUpdate = DatabaseAccessor.Instance.User
				.FirstOrDefault(t => t.UserId == updatedUser.UserId);

			if (userToUpdate != null)
			{
				userToUpdate.FirstName = updatedUser.FirstName;
				userToUpdate.LastName = updatedUser.LastName;
				userToUpdate.UserEmail = updatedUser.UserEmail;
				
			}
            else
            {
				return false;
			}

			DatabaseAccessor.Instance.SaveChanges();

			return true;
		}


	}

    public static class UserRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
			cfg.CreateMap<School.Database.Models.User, School.Repository.User>()
				.ForMember(dest => dest.UserClass, opt => opt.Ignore())
				.ReverseMap());



		public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Repository.User ToRepositoryModel(this School.Database.Models.User businessObject)
	    {
	        return mapper.Map<School.Repository.User>(businessObject);
	    }
	
	
	    public static School.Database.Models.User ToDbModel(this School.Repository.User repositoryObject)
	    {
	        return mapper.Map<School.Database.Models.User>(repositoryObject);
	    }
	
	}

}
