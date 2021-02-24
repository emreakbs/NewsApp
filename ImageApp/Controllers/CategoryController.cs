using ImageApp.Base;
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
            return View();
        }
        
        [Route("kategori-ekle")]
        public IActionResult AddCategory()
        {
            return View();
        }

        //public IActionResult AddCategory(CategoryModel categoryModel)
        //{
        //    if(!ModelState.IsValid) 
        //}

    }
}
