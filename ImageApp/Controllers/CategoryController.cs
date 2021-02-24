using ImageApp.Base;
using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Service.Category;
using ImageApp.Data.Model;
using ImageApp.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Controllers
{
    [Authorization]
    public class CategoryController : BaseController
    {
        [Route("/kategori-listele")]
        public IActionResult Index()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }

        [Route("kategori-ekle")]
        public IActionResult AddCategory()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Doğru kategori alanları bulunamadı.";
                return RedirectToAction("AddCategory");
            }

            var response = CategoryService.Instance.AddCategory(categoryModel, UserToken.UserTokenDto.Id);

            if (!response)
            {
                TempData["Error"] = "Kategori eklenemedi.";
                return RedirectToAction("AddCategory");
            }

            TempData["Success"] = "Kategori başarı ile eklenmiştir.";
            return RedirectToAction("Index");
        }

        [Route("kategori-düzenle/{id}")]
        public IActionResult EditCategory(int id)
        {
            if (id == 0)
            {
                TempData["Error"] = "Kategori belirlenemedi.";
                return RedirectToAction("Index");
            }

            var category = new CategoryEditDto
            {
                Category = CategoryService.Instance.GetCategory(id),
                CategoryList = CategoryService.Instance.GetCategoryList()
            };

            if (category.Category == null)
            {
                TempData["Error"] = "Kategori bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        /// <summary>
        /// Kategori düzenleme methodu
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditCategory(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Doğru kategori alanları bulunamadı.";
                return RedirectToAction("Index");
            }
            var response = CategoryService.Instance.EditCategory(categoryModel, UserToken.UserTokenDto.Id);

            if (!response)
            {
                TempData["Error"] = "Kategori düzenlenemedi.";
            }

            TempData["Success"] = "Kategori başarı ile düzenlenmiştir.";
            return RedirectToAction("Index");

        }
    }
}
