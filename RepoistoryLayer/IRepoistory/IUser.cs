using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoistoryLayer.IRepoistory
{
    public interface IUser
    {
        UserDetails Registration(UserRegistration userRegistration);
        UserDetails Login(UserLogin user);
    }
}
