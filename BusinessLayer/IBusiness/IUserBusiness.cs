using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.IBusiness
{
    public interface IUserBusiness
    {
        UserDetails Registration(UserRegistration user);
    }
}
