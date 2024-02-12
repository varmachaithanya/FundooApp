using System.Reflection.Metadata.Ecma335;
using BusinessLayer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBus bus;
        public UserController(IUserBusiness business, IBus bus)
        {
            this.business = business;
            this.bus = bus;
        }

        [HttpPost]
        [Route("RegisterUser")]

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

        [Authorize]
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
            //int userid = (int)login;
            if (login!=null)
            {
                //HttpContext.Session.SetInt32("UserId",userid);
                return Ok(login);
            }
            return BadRequest("Invalid Credentials");

        }

        [HttpPost]
        [Route("ForgotPassword")]

        public async Task<IActionResult> Forgot_PasswordAsync(string email)
        {
            var password = business.ForgotPassword(email);
            if (password!=null)
            {
                SendMsg send=new SendMsg();

                send.SendingMail(password.Email, "Password is Trying to Changed is that you....!\nToken: "+password.Token);

                Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                var endPoint = await bus.GetSendEndpoint(uri);
                //await endPoint.Send(ticket);


                return Ok("User Found"+ "\n Token:"+password.Token);

            }
            return Ok("User Not Found" + "\n Token:" + password.Token);

        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult Reset_Password(string Email,ResetPasswordModel model)
        {
            var reset = business.ResetPassword(Email,model);
            if (reset)
            {
                return Ok("Password Reset Sucessfully");
            }
            return BadRequest("Password Should Be Same");
        }



    }
}
