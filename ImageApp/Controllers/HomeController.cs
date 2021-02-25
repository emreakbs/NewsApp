using ImageApp.Base;
using ImageApp.Bussiness.Service;
using ImageApp.Bussiness.Service.Home;
using ImageApp.Helper;
using ImageApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Controllers
{
    [Authorization]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("~/")]
        public IActionResult Index()
        {
            var imageDictionary = HomeService.Instance.GetImages(2, 10);
            return View(imageDictionary);
        }

        [Route("gizlilik")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
