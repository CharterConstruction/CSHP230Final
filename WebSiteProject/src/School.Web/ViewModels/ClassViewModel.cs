using System;
using System.Collections.Generic;
using System.Linq;
using static School.Web.Models.ClassWebMapper;


namespace School.Web.ViewModels
{
    using Models;

    public interface IClassViewModel
	{
	    List<Class> Classes { get; }
	    Class Class(int id);
	} 
    
    public class ClassViewModel : IClassViewModel
{
    private readonly School.Business.IClassManager classManager;

    public ClassViewModel(School.Business.IClassManager classManager)
    {
        this.classManager = classManager;
    }


    public List<Class> Classes
    {
        get
        {
            return classManager.Classes.Select(t=>t.ToWebModel()).ToList();                                               
        }
    }


    public Class Class(int id)
    {
        return classManager.Classes.FirstOrDefault(t => t.ClassId == id).ToWebModel();
    } 
}   

}
