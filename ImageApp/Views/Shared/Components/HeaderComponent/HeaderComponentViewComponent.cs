using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Service.Category;
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
                HeaderComponentDto headerComponentDto = new HeaderComponentDto();
                var userInfo = HttpContext.Session.GetString("USER_INFO");
                UserTokenDto userTokenDto = JsonConvert.DeserializeObject<UserTokenDto>(userInfo);
                headerComponentDto.UserTokenDto = userTokenDto;
                headerComponentDto.CategoryList = CategoryService.Instance.GetCategoryList();
                return View("HeaderComponentView", headerComponentDto);
            }
            return Content(string.Empty);
            //return View("HeaderComponentView");
        }
    }
}
