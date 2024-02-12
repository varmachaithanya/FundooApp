using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using RepositoryLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public void InsertUser(UserModel request);

        public bool DeleteUser(string Firstname);

        public object GetUserDetails(long userid);

        public bool UpdateUserDetails(long userId, UserUpdateModel user);

        public object GetAllUsers();

        public string UserLogin(UserLoginModel user);

        public ForgotPasswordModel ForgotPassword(string email);

        public bool ResetPassword(string Email, ResetPasswordModel model);

    }
}
