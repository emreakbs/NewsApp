using ImageApp.Base;
using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Service.Category;
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
        [Route("resim-ekle")]
        public IActionResult AddImage()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }
        [HttpPost]
        public IActionResult AddImage(ImageDto imageDto)
        {
            return RedirectToAction("Index");
        }
    }
}
