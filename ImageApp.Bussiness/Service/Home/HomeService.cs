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
