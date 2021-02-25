using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Service.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Service.Home
{
    public class HomeService : IHomeService
    {
        #region Single Section

        private static readonly Lazy<HomeService> instance = new Lazy<HomeService>(() => new HomeService());
        public HomeService() { }
        public static HomeService Instance => instance.Value;

        #endregion
        /// <summary>
        /// Anasayfada kategorilere göre haberleri göstermeye yarar.
        /// </summary>
        /// <param name="count">kaç kategori ve her kategori için kaç adet haber getirileceğini belirtir</param>
        /// <returns>Haberler</returns>
        public Dictionary<string, List<ImageDto>> GetImages(int count)
        {
            Dictionary<string, List<ImageDto>> imageDictionary = new Dictionary<string, List<ImageDto>>();
            var categoryList = CategoryService.Instance.GetCategoryList(count);
            foreach (var category in categoryList)
            {
                var imageList = ImageService.Instance.GetImageList(category.Id, 1, count);
                imageDictionary.Add(category.CategoryName, imageList);
            }
            return imageDictionary;
        }
    }
}
