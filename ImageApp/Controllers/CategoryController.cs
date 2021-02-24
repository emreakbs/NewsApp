using ImageApp.Base;
using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Service.Category;
using ImageApp.Data.Enum;
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
        /// <summary>
        /// kategori liste sayfasını açar
        /// </summary>
        /// <returns></returns>
        [Route("/kategori-listele")]
        public IActionResult Index()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }
        /// <summary>
        /// kategori ekleme sayfasını açar
        /// </summary>
        /// <returns></returns>
        [Route("kategori-ekle")]
        public IActionResult AddCategory()
        {
            var categoryList = CategoryService.Instance.GetCategoryList();
            return View(categoryList);
        }
        /// <summary>
        /// kategori ekleme methodu
        /// </summary>
        /// <param name="categoryModel">eklenecek kategori modeli</param>
        /// <returns></returns>
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
        /// <summary>
        /// kategori düzenleme sayfasını açar
        /// </summary>
        /// <param name="id">kategori id</param>
        /// <returns></returns>
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
            //model uyum kontrolü
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Doğru kategori alanları bulunamadı.";
                return RedirectToAction("Index");
            }
            //kategori ve üst kategorinin aynı seçilme durumu
            if (categoryModel.Id == categoryModel.ParentCategory)
            {
                TempData["Error"] = "Üst kategori, kategori ile aynı olamaz.";
                return RedirectToAction("Index");
            }
            //Alt kategori seçili ilen parent kategori seçilmeme durumu
            if (categoryModel.CategoryType.Equals(CategoryType.Sub) && categoryModel.ParentCategory == 0)
            {
                TempData["Error"] = "Üst kategori seçilmedi.";
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
        /// <summary>
        /// Kategori silme methodu
        /// </summary>
        /// <param name="id">Kategori id</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0)
            {
                TempData["Error"] = "Kategori belirlenemedi.";
                return RedirectToAction("Index");
            }
            var response = CategoryService.Instance.DeleteCategory(id);

            if (!response)
            {
                TempData["Error"] = "Kategori silinemedi.";
            }

            TempData["Success"] = "Kategori başarı ile silinmiştir.";
            return RedirectToAction("Index");
        }
    }
}
