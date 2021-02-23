using ImageApp.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Base
{
    public class BaseController:Controller
    {
        protected Token UserToken => (Token)HttpContext.Items["USER_TOKEN"];
    }
}
