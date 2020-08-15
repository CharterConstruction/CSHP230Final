using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Web.Models
{    
    using School.Business;
    
    public interface IClassViewModel
	{
	    List<Class> Classs { get; }
	    Class Class(int id);
	} 
    
    public class ClassViewModel : IClassViewModel
{
    private readonly IClassManager classManager;

    public ClassViewModel(IClassManager classManager)
    {
        this.classManager = classManager;
    }


    public List<Class> Classs
    {
        get
        {
            return classManager.Classs.Select(t=>t.ToWebModel()).ToList();                                               
        }
    }


    public Class Class(int id)
    {
        return null;
    } 
}   

}
