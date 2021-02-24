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

        #endregion

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

        /// <summary>
        /// kategori düzenleme methodu
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool EditCategory(CategoryModel categoryModel, int userId)
        {
            categoryModel.UpdateUser = userId;
            categoryModel.UpdateDate = DateTime.UtcNow;

            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<CategoryModel>().Update(categoryModel);
            var result = uow.SaveChanges();

            return result > 0;
        }

        /// <summary>
        /// Tek olarak kategori döner
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public CategoryModel GetCategory(int categoryId)
        {
            using var uow = new UnitOfWork<MasterContext>();
            var result = uow.GetRepository<CategoryModel>().GetAll(x => x.Id == categoryId).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Kategori listesini döner
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> GetCategoryList()
        {
            using var uow = new UnitOfWork<MasterContext>();
            var categoryList = uow.GetRepository<CategoryModel>().GetAll().ToList();
            return categoryList;
        }


    }
}
