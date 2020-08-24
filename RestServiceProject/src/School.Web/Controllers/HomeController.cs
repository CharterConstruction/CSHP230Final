using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Web.Models;
using School.Business;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace School.Web.Controllers
{
    using ViewModels;

    public class HomeController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IUserViewModel userViewModel;
        private readonly IClassViewModel classViewModel;
        private readonly IUserClassManager userClassManager;

        public HomeController(IUserManager userManager, IUserViewModel userViewModel, IClassViewModel classViewModel, IUserClassManager userClassManager)
        {
            this.userManager = userManager;
            this.userViewModel = userViewModel;
            this.classViewModel = classViewModel;
            this.userClassManager = userClassManager;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public ActionResult LogIn()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }




        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);
                

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new School.Web.Models.User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserId = user.UserId,
                        UserEmail = user.UserEmail
                    });

                    HttpContext.Session.SetString("User", json);
                    
                    userViewModel.CurrentUser = user.ToWebModel();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserEmail),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginModel);
        }



        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            userViewModel.CurrentUser = null;

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }





        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.FirstName, registerModel.LastName, registerModel.UserEmail, registerModel.Password);

                userViewModel.CurrentUser = user.ToWebModel();

                return Redirect("~/");
            }
            else
            {
                return View();
            }
        }




        
        public ActionResult Classes()
        {
            var session = HttpContext.Session;

            List<Models.Class> availableClasses = new List<Models.Class>();

            if (User.Identity.IsAuthenticated)
            {
                var userClassIds = GetUserClasses().Select(t=>t.ClassId);
                availableClasses = classViewModel.Classes.Where(t => !userClassIds.Contains(t.ClassId)).ToList();
            }
            else
            {
                availableClasses = classViewModel.Classes;
            }

            return View(availableClasses);    
        }

        
        [Authorize]
        public ActionResult UserClasses()
        {            
            var userClasses = GetUserClasses();

            if (!userClasses.Any())
            {
                TempData["Message"] = $"You are not enrolled in any classes";
            }
            else
            {
                TempData["Message"] = $"You are enrolled in the following {userClasses.Count} classes:{Environment.NewLine}";
            }

            return View(userClasses);
        }




        [Authorize]
        public ActionResult Enroll(int classId)
        {

            var existingEnrolledClassIds = GetUserClasses().Select(t => t.ClassId);

            if (existingEnrolledClassIds.Contains(classId))
            {
                var existingClass = classViewModel.Class(classId);                
                TempData["Message"] = $"You are already enrolled in {existingClass.ClassName}.";

                return RedirectToAction("Classes");
            }

            var user = JsonConvert.DeserializeObject<Models.User>(HttpContext.Session.GetString("User"));

            var userClass = userClassManager.Add(user.UserId, classId).ToWebModel();

            return RedirectToAction("UserClasses");

  

        }

        [Authorize]
        public ActionResult DropOut(int classId)
        {
            var user = GetLoggedOnUser();
            var existingEnrolledClassIds = GetUserClasses().Select(t => t.ClassId);
            var targetClass = classViewModel.Class(classId);

            if(targetClass == null)
            {
                TempData["Error"] = $"No class was found with ID: {classId}";
                return RedirectToAction("UserClasses");
            }

            if (existingEnrolledClassIds.Contains(classId))
            {
                var isClassRemoved = userClassManager.Remove(user.UserId, classId);
                

                if (isClassRemoved)
                {                 
                    TempData["Success"] = $"You have successfully dropped out of '{targetClass.ClassName}'";
                }
                else
                {
                    TempData["Error"] = $"Your request to drop out of class '{targetClass.ClassName}' has failed.  Please contact IT.";
                }                            

            }
            else
            {
                TempData["Error"] = $"You are not enrolled in '{targetClass.ClassName}'.  Ignoring request.";                
            }

            return RedirectToAction("UserClasses");
        }




        private Models.User GetLoggedOnUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return JsonConvert.DeserializeObject<Models.User>(HttpContext.Session.GetString("User"));
            }
            else
            {
                return null;
            }


            //return JsonConvert.DeserializeObject<Models.User>(HttpContext.Session.GetString("User"));
        }


        private List<Models.UserClass> GetUserClasses()
        {
            var user = GetLoggedOnUser();
            return userClassManager.GetUserClasses(user.UserId).Select(t => t.ToWebModel()).ToList();
        }





    }
}
