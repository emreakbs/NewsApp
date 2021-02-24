using ImageApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Dto
{
   public class CategoryEditDto
    {
        public CategoryModel Category { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
    }
}
