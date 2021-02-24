using ImageApp.Base;
using ImageApp.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    [Authorization]
    public class ImageController : BaseController
    {
        [Route("resim-liste")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
