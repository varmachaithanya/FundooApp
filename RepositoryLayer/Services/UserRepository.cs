using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using static System.Formats.Asn1.AsnWriter;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FundoContext context;
        private readonly IConfiguration _config;

        public UserRepository(FundoContext context, IConfiguration _config)
        {
            this.context = context;
            this._config = _config;
        }

        public static string EncodePassword(string password)
        {
            var encodedData = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encodedData);
        }

        public static string DecodePassword(string password)
        {
            var decodeData = Convert.FromBase64String(password);
            return Encoding.UTF8.GetString(decodeData);
        }

        private string GenerateToken(long UserId, string userEmail)
        {
            // Create a symmetric security key using the JWT key specified in the configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Define claims to be included in the JWT
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserId", UserId.ToString())
            };
            // Create a JWT with specified issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public void InsertUser(UserModel request)
        {
            User user = new User();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = EncodePassword(request.Password);

            context.Users.Add(user);
            context.SaveChanges();


        }

        public bool DeleteUserAccount(string Firstname)
        {
            var user = context.Users.FirstOrDefault(x => x.FirstName == Firstname);
            if (user == null)
            {
                return false;
            }
            context.Users.Remove(user);
            context.SaveChanges();
            return true;

        }

        public object GetUserDetails(long userId)
        {
            var getData = context.Users.FirstOrDefault(x => x.UsertId == userId);
            if (getData == null)
            {
                return null;
            }
            return getData;
        }

        public bool UpdateUserDetails(long userId, UserUpdateModel user)
        {
            var update = context.Users.FirstOrDefault(x => x.UsertId == userId);
            if (update != null)
            {
                update.FirstName = user.FirstName;
                update.LastName = user.LastName;
                update.Email = user.Email;
                context.SaveChanges();


                return true;
            }
            return false;
        }

        //For Task Purpose
        public string UpdateUser(long userId, UserModel user)
        {
            var update = context.Users.FirstOrDefault(x => x.UsertId == userId);
            if (update != null)
            {
                update.FirstName = user.FirstName;
                update.LastName = user.LastName;
                update.Email = user.Email;
                update.Password = EncodePassword(user.Password);

                context.SaveChanges();


                return "Updated";
            }
            return "NotFound";
        }



        public object GetAllUsers()
        {
            var data = context.Users.ToList();

            if (data == null)
            {
                return null;
            }

            return data;
        }

        public string UserLogin(UserLoginModel user)
        {
            var userLogin = context.Users.FirstOrDefault(x => x.Email == user.Email);
            string userpass = DecodePassword(userLogin.Password);

            if (userLogin.Email.Equals(user.Email) && userpass.Equals(user.Password))
            {
                //HttpContext.Session.SetInt32("UserId", userLogin.UsertId);

                var token = GenerateToken(userLogin.UsertId, user.Email);

                return token;
            }
            return "Not Found...";
        
        }


        public ForgotPasswordModel ForgotPassword(string email)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email);

            if(user != null)
            {
                ForgotPasswordModel model = new ForgotPasswordModel();

                model.Email = user.Email;
                model.UserId = user.UsertId;
                model.Token = GenerateToken(user.UsertId, user.Email);
                return model;

            }
            return null;
        }

        public bool ResetPassword(string Email,ResetPasswordModel model)
        {
            var reset=context.Users.FirstOrDefault(x=> x.Email == Email);
            if (model.NewPassword.Equals(model.ConfirmPassword))
            {
                reset.Password = EncodePassword(model.NewPassword);
                context.SaveChanges();
                return true;
            }
            return false;
        }


    }
}