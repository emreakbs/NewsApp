﻿using ImageApp.Base;
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
        /// <summary>
        /// içerik listesini açar
        /// </summary>
        /// <returns></returns>
        [Route("icerik-liste")]
        public IActionResult Index()
        {
            var imageDtoList = ImageService.Instance.GetAllImage();
            return View(imageDtoList);
        }
        /// <summary>
        /// içerik ekleme sayfasını açar
        /// </summary>
        /// <returns></returns>
        [Route("icerik-ekle")]
        public IActionResult AddImage()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }
        /// <summary>
        /// içerik ekleme işlemini yapar.
        /// </summary>
        /// <param name="imageDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddImage(ImageDto imageDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Uygun format bulunamadı.";
                return RedirectToAction("AddImage");
            }
            ImageService.Instance.AddImage(imageDto, UserToken.UserTokenDto.Id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// içerik görüntüleme işlemini yapar
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Route("icerik/{url}")]
        public IActionResult DetailImage(string url)
        {
            var category = ImageService.Instance.GetImage(url);
            return View(category);
        }
        /// <summary>
        /// içerik silem işlemini yapar
        /// </summary>
        /// <param name="id">içerik id'si</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteImage(int id)
        {
            if (id == 0)
            {
                TempData["Error"] = "İçerik belirlenemedi.";
                return RedirectToAction("Index");
            }
            var response = ImageService.Instance.DeleteImage(id);

            if (!response)
            {
                TempData["Error"] = "İçerik silinemedi.";
            }

            TempData["Success"] = "İçerik başarı ile silinmiştir.";
            return RedirectToAction("Index");
        }
        [Route("icerik-duzenle/{id}")]
        public IActionResult EditImage(int id)
        {
            if (id == 0)
            {
                TempData["Error"] = "İçerik bulunamadı.";
                RedirectToAction("Index");
            }
            var image = ImageService.Instance.GetImage(id);
            ViewBag.CategoryList = CategoryService.Instance.GetCategoryList();
            if (image == null)
            {
                TempData["Error"] = "İçerik bulunamadı.";
                RedirectToAction("Index");
            }
            return View(image);
        }
        [HttpPost]
        public IActionResult EditImage(ImageDto imageDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Düzenlenen içerik uygun formatta değil.";
                return RedirectToAction("Index");
            }
            var response = ImageService.Instance.EditImage(imageDto, UserToken.UserTokenDto.Id);

            if (response) TempData["Success"] = "İçerik başarı ile düzenlendi.";
            else TempData["Error"] = "Beklenmeyen hata. İçerik düzenlenemedi.";

            return RedirectToAction("Index");
        }

    }
}
