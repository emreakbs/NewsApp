using ImageApp.Bussiness.Extension;
using ImageApp.Bussiness.Service;
using ImageApp.Core.Dto;
using ImageApp.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Helper
{
    public class AuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Cookies["UserToken"];
            //token yoksa
            if (string.IsNullOrEmpty(token)) { RedirectForbiddenLogin(context); return; }
            //token varsa
            else
            {
                var currentUser = JsonConvert.DeserializeObject<Token>(token);

                var tokenControl = LoginService.Instance.TokenValidate(currentUser.UserTokenDto.Id, currentUser.AccessToken);
                //token geçerli değilse
                if (!tokenControl) { RedirectForbiddenLogin(context); return; }

                context.HttpContext.Items.Add("USER_TOKEN", currentUser);
                context.HttpContext.Session.SetString("USER_INFO", JsonConvert.SerializeObject(currentUser.UserTokenDto));
            }
        }
        /// <summary>
        /// Giriş sayfasına yönlendirir.
        /// </summary>
        /// <param name="context"></param>
        private void RedirectForbiddenLogin(AuthorizationFilterContext context)
        {
            string applicationUrl = Environment.GetEnvironmentVariable("APP_URL");
            //string applicationUrl = "http://localhost:18000";
            context.Result = new RedirectResult($"{applicationUrl}/Login/Index");
        }

    }
}
