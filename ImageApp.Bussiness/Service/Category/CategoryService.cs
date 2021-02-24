using System;
using System.Collections.Generic;
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

    }
}
