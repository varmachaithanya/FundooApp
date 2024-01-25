using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FundoContext context;
        public UserRepository(FundoContext context)
        {
            this.context = context;
        }

        public void InsertUser(UserModel request)
        {
            User user = new User();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;

            context.Users.Add(user);
            context.SaveChanges();


        }

        public bool DeleteUserAccount(string Firstname)
        {
            var user = context.Users.FirstOrDefault(x=>x.FirstName==Firstname);
            if (user == null)
            {
                return false;
            }
            context.Users.Remove(user);
            context.SaveChanges();
            return true;

        }

        public object  GetUserDetails(long userId)
        {
            var getData = context.Users.FirstOrDefault(x=>x.UsertId==userId);
            if(getData == null)
            {
                return null;
            }
            return getData;
        }

        public bool UpdateUserDetails(long userId, UserUpdateModel user)
        {
            var update=context.Users.FirstOrDefault(x=>x.UsertId==userId);
            if (update != null) 
            {
                update.FirstName= user.FirstName;
                update.LastName= user.LastName;
                update.Email = user.Email;
                context.SaveChanges();


                return true;
            }
            return false;
        }

        public object GetAllUsers()
        {
            var data = context.Users.ToList();

            if(data == null)
            {
                return null;
            }

            return data;
        }

        public bool UserLogin(UserLoginModel user)
        {
            var userLogin=context.Users.FirstOrDefault(x=>x.Email==user.Email);
            if (userLogin.Email==user.Email && userLogin.Password==user.Password) 
            {
                return true;
            }
            return false;
        }

        
    }
}