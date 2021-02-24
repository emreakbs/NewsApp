using ImageApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Service.Category
{
    public interface ICategoryService
    {
        List<CategoryModel> GetCategoryList();
        bool AddCategory(CategoryModel categoryModel, int userId);
        CategoryModel GetCategory(int categoryId);
        bool EditCategory(CategoryModel categoryModel, int userId);
    }
}
