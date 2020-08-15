using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Web.Models
{    
    using School.Business;
    
    public interface IUserViewModel
	{
	    List<User> Users { get; }
	    User User(int id);
	} 
    
    public class UserViewModel : IUserViewModel
{
    private readonly IUserManager userManager;

    public UserViewModel(IUserManager userManager)
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
}   

}
