using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public void InsertUser(UserModel request);

        public bool DeleteUserAccount(string Firstname);

        public object GetUserDetails(long userId);

        //For Task Purpose
        public string UpdateUser(long userId, UserModel user);


        public bool UpdateUserDetails(long userId, UserUpdateModel user);

        public object GetAllUsers();

        public string UserLogin(UserLoginModel user);

        public ForgotPasswordModel ForgotPassword(string email);

        public bool ResetPassword(string Email, ResetPasswordModel model);

    }
}
