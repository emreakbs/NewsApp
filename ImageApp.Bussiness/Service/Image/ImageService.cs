using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Extension;
using ImageApp.Bussiness.Helper;
using ImageApp.Bussiness.Service.Image;
using ImageApp.Core.Model;
using ImageApp.Data;
using ImageApp.Data.Model;
using ImageApp.DataAccess.Repository.MongoRepository;
using ImageApp.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Service
{
    public class ImageService : IImageService
    {
        #region Single Section

        private static readonly Lazy<ImageService> instance = new Lazy<ImageService>(() => new ImageService());
        public ImageService() { }
        public static ImageService Instance => instance.Value;

        #endregion
        /// <summary>
        /// Haber eklemeye yarar
        /// </summary>
        /// <param name="imageDto"></param>
        /// <returns></returns>
        public bool AddImage(ImageDto imageDto, int userId)
        {

            using var uow = new UnitOfWork<MasterContext>();
            var efResult = false;
            var imageModel = ObjectMapper.Map<ImageModel>(imageDto);
            imageModel = BaseDatabaseOperations.Instance.SetCreateValues(imageModel, userId);
            imageModel.RouteUrl = UrlControl(uow, imageDto);
            
            uow.GetRepository<ImageModel>().Add(imageModel);
            
            var mongoResult = AddImageMongo(imageModel, imageDto.LargeImage, imageDto.SmallImage);
            if (mongoResult) efResult = uow.SaveChanges() > 0;

            return true;
        }
        /// <summary>
        /// Mongo'ya büyük ve küçük resmi ekler
        /// </summary>
        /// <param name="imageModel">içerik modeli</param>
        /// <param name="largeImage">Büyk resim</param>
        /// <param name="smallImage">Küçk resim</param>
        private bool AddImageMongo(ImageModel imageModel, string largeImage, string smallImage)
        {
            using (MongoRepository<ImageMongoModel> mongoRepository = new MongoRepository<ImageMongoModel>())
            {
                var imageMongoModel = new ImageMongoModel
                {
                    CreateDate = imageModel.CreateDate,
                    CreateUserId = imageModel.CreateUser,
                    UpdateDate = imageModel.UpdateDate,
                    UpdateUserId = imageModel.UpdateUser,
                    DatabaseName = "ImageApp",
                    ParentId = imageModel.Id,
                    LargeImage = largeImage,
                    SmallImage = smallImage,
                    TableName = "Images"
                };
                return mongoRepository.Add(imageMongoModel);
            }
        }

        /// <summary>
        /// url'in daha önceden eklenip eklenmediğini kontrol eder
        /// </summary>
        /// <param name="uow">UnitOfWork</param>
        /// <param name="imageDto">İçerik</param>
        /// <returns>Kullanılabilecek URL</returns>
        private string UrlControl(UnitOfWork<MasterContext> uow, ImageDto imageDto)
        {
            var urlCount = uow.GetRepository<ImageModel>().Count(x => x.RouteUrl.Equals(imageDto.RouteUrl));
            if (urlCount > 0)
                imageDto.RouteUrl = $"{imageDto.RouteUrl}-{urlCount}";

            return imageDto.RouteUrl;
        }


    }
}
