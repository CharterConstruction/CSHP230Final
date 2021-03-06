using System;
using System.Collections.Generic;
using System.Linq;
using static School.Web.Models.UserWebMapper;
    

namespace School.Web.ViewModels
{
        using Models;

        public interface IUserViewModel
	    {
	        List<User> Users { get; }
	        User User(int id);

            User CurrentUser { get; set; }       

	    } 
    
    public class UserViewModel : IUserViewModel
    {
        public readonly School.Business.IUserManager userManager;

        public UserViewModel(School.Business.IUserManager userManager)
        {
            this.userManager = userManager;            
        }


        public List<User> Users
        {
            get
            {
                return userManager.Users.Select(t=>t.ToWebModel()).ToList();                                               
            }
        }


        public User User(int id)
        {
            return null;
        }

        



        public User CurrentUser { get; set; }

    }   

}
