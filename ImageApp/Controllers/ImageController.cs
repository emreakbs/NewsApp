using ImageApp.Base;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    [Route("resim")]
    public class ImageController : BaseController
    {
        [Route("liste")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
