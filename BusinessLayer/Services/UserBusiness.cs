using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _repository;
        public UserBusiness(IUserRepository repository)
        {
            _repository=repository;
        }


         public void InsertUser(UserModel request)
        {
            _repository.InsertUser(request);
        }

        public bool DeleteUser(string Firstname)
        {
            return _repository.DeleteUserAccount(Firstname);
            
            

        }

        public object GetUserDetails(long userid)
        {
            return _repository.GetUserDetails(userid);
        }

        public bool UpdateUserDetails(long userId, UserUpdateModel user)
        {
            return _repository.UpdateUserDetails(userId, user);
        }

        public object GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public string UserLogin(UserLoginModel user)
        {
            return (_repository.UserLogin(user));
        }

        public ForgotPasswordModel ForgotPassword(string email)
        {
            return (_repository.ForgotPassword(email));
        }

        public bool ResetPassword(string Email, ResetPasswordModel model)
        {
            return _repository.ResetPassword(Email,model);
        }

    }


}
