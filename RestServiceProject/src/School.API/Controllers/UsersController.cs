using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.API.Controllers
{
    //using Business;
    using Models;


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly Business.IUserManager userManager;
        private readonly Business.IClassManager classManager;
        private readonly Business.IUserClassManager userClassManager;
        

        public UsersController(Business.IUserManager userManager, Business.IClassManager classManager, Business.IUserClassManager userClassManager)
        {
            this.userManager = userManager;
            this.classManager = classManager;            
            this.userClassManager = userClassManager;
        }


        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = userManager.Users.Select(t=>t.ToWebModel());
            return users;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userManager.User(id).ToWebModel();
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterModel value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }

            var result = userManager.Register(value.FirstName, value.LastName, value.UserEmail, value.Password);


            if(result == null)
            {
                return new BadRequestResult();
            }
            else
            {
                School.API.Models.User newUser = result.ToWebModel();


                return CreatedAtAction(nameof(Get),
                   new { id = newUser.UserId },
                    newUser);
            }

            //var result = new { Id = value.Id, Candy = true };

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (ModelState.IsValid && id > 0 && value != null)
            {
                var targetUser = userManager.User(id);
                targetUser.FirstName = value.FirstName;
                targetUser.LastName = value.LastName;
                targetUser.UserEmail = value.UserEmail;

                var updateResult = userManager.Update(targetUser);
                return Ok(updateResult);
            }
            else
            {
                return new BadRequestResult();
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return userManager.Remove(id);
        }
    }
}
