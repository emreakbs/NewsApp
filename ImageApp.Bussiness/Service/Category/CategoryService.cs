using ImageApp.Bussiness.Helper;
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
        /// Kategori eklemeye servisi.
        /// </summary>
        /// <param name="categoryModel">Kategori model</param>
        /// <param name="userId">User id</param>
        /// <returns>Boolean</returns>
        public bool AddCategory(CategoryModel categoryModel, int userId)
        {
            categoryModel = BaseDatabaseOperations.Instance.SetCreateValues(categoryModel, userId);
            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<CategoryModel>().Add(categoryModel);
            var result = uow.SaveChanges();

            return result > 0;
        }

        /// <summary>
        /// Kategori silmeye servisi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCategory(int id)
        {
            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<CategoryModel>().Delete(x => x.Id == id);
            var response = uow.SaveChanges();
            return response > 0;
        }

        /// <summary>
        /// kategori düzenleme servisi
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool EditCategory(CategoryModel categoryModel, int userId)
        {
            categoryModel = BaseDatabaseOperations.Instance.SetUpdateValues(categoryModel, userId);

            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<CategoryModel>().Update(categoryModel);
            var result = uow.SaveChanges();

            return result > 0;
        }

        /// <summary>
        /// Tek olarak kategori dönen servis
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
        /// Kategori listesini dönen servis
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> GetCategoryList()
        {
            using var uow = new UnitOfWork<MasterContext>();
            var categoryList = uow.GetRepository<CategoryModel>().GetAll().OrderByDescending(o => o.Id).ToList();
            return categoryList;
        }

        /// <summary>
        /// istenilen adet kadar kategori döner
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<CategoryModel> GetCategoryList(int count)
        {
            using var uow = new UnitOfWork<MasterContext>();
            var categoryList = uow.GetRepository<CategoryModel>().GetAll().OrderByDescending(o => o.Id).Take(count).ToList();
            return categoryList;
        }



    }
}
