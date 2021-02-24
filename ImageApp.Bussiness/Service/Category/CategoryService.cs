using ImageApp.Data;
using ImageApp.Data.Model;
using ImageApp.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageApp.Bussiness.Service.Category
{
    public class CategoryService : ICategoryService
    {

        #region Single Section

        private static readonly Lazy<CategoryService> instance = new Lazy<CategoryService>(() => new CategoryService());
        public CategoryService() { }
        public static CategoryService Instance => instance.Value;
        /// <summary>
        /// Kateori eklemeye yarar.
        /// </summary>
        /// <param name="categoryModel">Kategori model</param>
        /// <param name="userId">User id</param>
        /// <returns>Boolean</returns>
        public bool AddCategory(CategoryModel categoryModel, int userId)
        {
            categoryModel.CreateUser = userId;
            categoryModel.UpdateUser = userId;
            categoryModel.CreateDate = DateTime.UtcNow;
            categoryModel.UpdateDate = DateTime.UtcNow;

            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<CategoryModel>().Add(categoryModel);
            var result = uow.SaveChanges();

            return result > 0;
        }

        public List<CategoryModel> GetCategoryList()
        {

            using var uow = new UnitOfWork<MasterContext>();
            var categoryList = uow.GetRepository<CategoryModel>().GetAll().ToList();
            return categoryList;
        }

        #endregion

    }
}
