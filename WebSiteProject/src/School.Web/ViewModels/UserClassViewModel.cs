using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Web.Models
{    
    using School.Business;
    
    public interface IUserClassViewModel
	{
	    List<UserClass> UserClasss { get; }
	    UserClass UserClass(int id);
	} 
    
    public class UserClassViewModel : IUserClassViewModel
{
    private readonly IUserClassManager userClassManager;

    public UserClassViewModel(IUserClassManager userClassManager)
    {
        this.userClassManager = userClassManager;
    }


    public List<UserClass> UserClasss
    {
        get
        {
            return userClassManager.UserClasss.Select(t=>t.ToWebModel()).ToList();                                               
        }
    }


    public UserClass UserClass(int id)
    {
        return null;
    } 
}   

}
