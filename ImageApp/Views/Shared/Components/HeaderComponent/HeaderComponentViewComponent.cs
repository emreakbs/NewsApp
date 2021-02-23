using ImageApp.Bussiness.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Views.Shared.Components.HeaderComponent
{
    public class HeaderComponentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("USER_INFO")))
            {
                var userInfo = HttpContext.Session.GetString("USER_INFO");
                UserTokenDto userTokenDto = JsonConvert.DeserializeObject<UserTokenDto>(userInfo);
                return View("HeaderComponentView", userTokenDto);
            }
            return Content(string.Empty);
            //return View("HeaderComponentView");
        }
    }
}
