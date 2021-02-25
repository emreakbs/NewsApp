using ImageApp.Base;
using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Service;
using ImageApp.Bussiness.Service.Category;
using ImageApp.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    [Authorization]
    public class ImageController : BaseController
    {
        [Route("icerik-liste")]
        public IActionResult Index()
        {
            var imageDtoList = ImageService.Instance.GetAllImage();
            return View(imageDtoList);
        }
        [Route("icerik-ekle")]
        public IActionResult AddImage()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }
        [HttpPost]
        public IActionResult AddImage(ImageDto imageDto)
        {
            ImageService.Instance.AddImage(imageDto, UserToken.UserTokenDto.Id);
            return RedirectToAction("Index");
        }
        [Route("icerik-goruntule/{url}")]
        public IActionResult DetailImage(string url)
        {
            var category = ImageService.Instance.GetImage(url);
            return View(category);
        }
    }
}
