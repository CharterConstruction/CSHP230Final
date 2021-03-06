

using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository
{
    public interface IUserClassRepository
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

    public class UserClassRepository : IUserClassRepository
	{
		public UserClass Add(int userId, int classId)
		{
			 Repository.UserClass addedClass = new UserClass();

			var targetUser = DatabaseAccessor.Instance.User.FirstOrDefault(t => t.UserId == userId);
			var targetClass = DatabaseAccessor.Instance.Class.FirstOrDefault(t => t.ClassId == classId);

			if (targetClass == null || targetUser == null)
			{
				return null;
			}
			else
			{
				var addedClassToDatabase = DatabaseAccessor.Instance.UserClass.Add(
					new Database.Models.UserClass()
					{
						Class = targetClass,
						ClassId = targetClass.ClassId,
						User = targetUser,
						UserId = targetUser.UserId
					});
				
				DatabaseAccessor.Instance.SaveChanges();

				addedClass.Class = targetClass.ToRepositoryModel();
				addedClass.ClassId = targetClass.ClassId;
				addedClass.User = targetUser.ToRepositoryModel();
				addedClass.UserId = targetUser.UserId;

			}
			
			return addedClass;
		}

		public bool Remove(int userId, int classId)
		{
			var itemsToRemove = DatabaseAccessor.Instance.UserClass
					.Where(t => t.UserId == userId && t.ClassId == classId);

			if (itemsToRemove.Count() == 0)
			{
				return false;
			}

			DatabaseAccessor.Instance.UserClass.Remove(itemsToRemove.First());

			DatabaseAccessor.Instance.SaveChanges();

			return true;

		}


		public List<UserClass> GetUserClasses(int userId)
	    {
			return DatabaseAccessor.Instance.UserClass
				.Where(t=>t.UserId == userId)
				.Select(t=>
					
					new UserClass()
                    {
						Class = t.Class.ToRepositoryModel(),
						ClassId = t.ClassId,
						User = t.User.ToRepositoryModel(),
						UserId = t.UserId
					}					
				)
				.ToList();
	    }
	
	}

    public static class UserClassRepositoryMapper
	{    
	    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<School.Database.Models.UserClass, School.Repository.UserClass>().ReverseMap());
	    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
	
	
	    public static School.Repository.UserClass ToRepositoryModel(this School.Database.Models.UserClass businessObject)
	    {
			var targetUser = DatabaseAccessor.Instance.User.FirstOrDefault(t => t.UserId == businessObject.UserId).ToRepositoryModel();
			var targetClass = DatabaseAccessor.Instance.Class.FirstOrDefault(t => t.ClassId == businessObject.ClassId).ToRepositoryModel();

			return new School.Repository.UserClass()
			{
				Class = targetClass,
				ClassId = targetClass.ClassId,
				User = targetUser,
				UserId = targetUser.UserId
			};
	    }
		


		public static School.Database.Models.UserClass ToDbModel(this School.Repository.UserClass repositoryObject)
	    {

			var targetUser = DatabaseAccessor.Instance.User.FirstOrDefault(t => t.UserId == repositoryObject.UserId);
			var targetClass = DatabaseAccessor.Instance.Class.FirstOrDefault(t => t.ClassId == repositoryObject.ClassId);

			return new Database.Models.UserClass()
			{
				Class = targetClass,
				ClassId = targetClass.ClassId,
				User = targetUser,
				UserId = targetUser.UserId
			};

	    }
	
	}

}
