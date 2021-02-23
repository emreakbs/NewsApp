using ImageApp.Base;
using ImageApp.Bussiness.Service;
using ImageApp.Core.Model;
using ImageApp.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Controllers
{
    public class LoginController : BaseController
    {
        /// <summary>
        /// Kullanıcı kaydetme methodu
        /// </summary>
        /// <param name="user"></param>
        /// <returns>StatusCode</returns>
        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = LoginService.Instance.Create(user);
            return result ? StatusCode(201) : StatusCode(500);
        }

        /// <summary>
        /// Kullanıcı giriş metodu
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Token</returns>
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            if (String.IsNullOrEmpty(userModel.UserName) || String.IsNullOrEmpty(userModel.Password))
            {
                TempData["Error"] = "Kullanıcı adı veya şifre alanı boş olamaz.";
                return RedirectToAction("Index");
            }

            var token = LoginService.Instance.Login(userModel);
            if (token == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index");
            }

            HttpContext.Response.Cookies.Append("UserToken", JsonConvert.SerializeObject(token), new CookieOptions()
            {
                Domain = Environment.GetEnvironmentVariable("COOKIE_DOMAIN"),
                Expires = token.Expiration
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
