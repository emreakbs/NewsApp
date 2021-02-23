using ImageApp.Base;
using ImageApp.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Controllers
{
    [Authorization]
    [Route("hata")]
    public class ErrorController : BaseController
    {
        [Route("404")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
