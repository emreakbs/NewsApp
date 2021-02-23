using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Helper
{
    public class AuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Cookies["UserToken"];

            Console.WriteLine(token);
            if (string.IsNullOrEmpty(token))
            {
                token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(token))
                {
                    RedirectForbiddenLogin(context);
                    return;
                }
            }

            UserDto currentUser = UserProxy.Instance.ValidateToken(token);
            if (currentUser == null)
            {
                RedirectForbiddenLogin(context);
                return;
            }

            context.HttpContext.Items.Add("CURRENT_USER", currentUser);
            context.HttpContext.Items.Add("USER_TOKEN", token);
            ExtentionMethods.SetTimeZone(currentUser.TimeZone);
        }

        private void RedirectForbiddenLogin(AuthorizationFilterContext context)
        {
            string applicationUrl = Environment.GetEnvironmentVariable("APP_URL"); //ex. "http://test.Niplocal:16601"
            string userApplicationUrl = Environment.GetEnvironmentVariable("USER_URL"); //ex. "http://user.Niplocal:16600"
            context.Result = "true".Equals(context.HttpContext.Request.Query["iframe"])
                ? new RedirectResult($"{userApplicationUrl}/Login/ForbiddenLogin/?redirectUrl={applicationUrl}{context.HttpContext.Request.Path}?iframe=true")
                : new RedirectResult($"{userApplicationUrl}/Login/ForbiddenLogin/?redirectUrl={applicationUrl}{context.HttpContext.Request.Path}");
        }

    }
}
