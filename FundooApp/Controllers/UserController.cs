using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer;
using RepositoryLayer.Entities;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness business;
        public UserController(IUserBusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        [Route("CreateUser")]

        public IActionResult InsertUser(UserModel request)
        {
           business.InsertUser(request);
          
            return Ok(request);

        }

        [HttpDelete]
        [Route("DeleteUser")]

        public IActionResult DeleteUser(string Firstname) 
        {
            var deleteUserData=business.DeleteUser(Firstname);

            if (deleteUserData)
            {
                return Ok("User Deleted Succesfully...");
            }
            return BadRequest("User Not Found...");
        }

        [HttpGet]
        [Route("Getuser")]
        public IActionResult GetUser(long userid) 
        {
            var getUserData=business.GetUserDetails(userid);

            if(getUserData== null)
            {
                return BadRequest("User Not Found...");
            }

            return Ok(getUserData);

            
        }

        [HttpPut]
        [Route("UpdateUser")]

        public IActionResult UpdateUser(long userid, UserUpdateModel user)
        {
            var updateUserData = business.UpdateUserDetails(userid,user);

            if(updateUserData)
            {
                return Ok(user);
            }
            return BadRequest("User Not Found...");
        }

        [HttpGet]
        [Route("GetAllUsers")]

        public IActionResult GetUsers()
        {
            var getAllUsers = business.GetAllUsers();

            if(getAllUsers== null)
            {
                return BadRequest("Users Not Found...");
            }

            return Ok(getAllUsers);
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult LoginUser(UserLoginModel user) 
        {
            var login = business.UserLogin(user);
            if (login)
            {
                return Ok("Sucessfully LoggedIn");

            }
            return BadRequest("Invalid Credentials");

        }

        
    }
}
