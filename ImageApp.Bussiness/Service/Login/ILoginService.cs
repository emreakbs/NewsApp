using ImageApp.Core.Model;
using ImageApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Service.Login
{
    public interface ILoginService
    {
        bool Create(UserModel userModel);
        Token Login(UserModel userModel);
        bool TokenValidate(int id, string token);
        bool Logout(int id);
    }
}
