using BusinessLayer.IBusiness;
using CommonLayer.Models;
using RepoistoryLayer.IRepoistory;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUser users;

        public UserBusiness(IUser users)
        {
            this.users = users;
        }

        public UserDetails Registration(UserRegistration user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("It cannot be null");
                }
                else if (user.FirstName == "" || user.LastName == "" || user.Email == "" || user.Password == "" || user.Address == "" || user.City == "" || user.PhoneNumber == "")
                {
                    throw new Exception("It cannot be Empty");
                }

                var result = users.Registration(user);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public UserDetails Login(UserLogin user)
        {
            try
            {
                var result = users.Login(user);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
