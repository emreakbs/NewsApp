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
using System.Linq;
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

            var imageModel = ObjectMapper.Map<ImageModel>(imageDto);
            imageModel = BaseDatabaseOperations.Instance.SetCreateValues(imageModel, userId);
            imageModel.RouteUrl = UrlControl(uow, imageDto);

            uow.GetRepository<ImageModel>().Add(imageModel);
            var efResult = uow.SaveChanges() > 0;

            var mongoResult = false;
            if (efResult) mongoResult = AddImageMongo(imageModel, imageDto.LargeImage, imageDto.SmallImage);

            return true;
        }

        public List<ImageDto> GetAllImage(bool imageShow)
        {
            List<ImageDto> imageDtoList = new List<ImageDto>();

            using var uow = new UnitOfWork<MasterContext>();
            var imageList = uow.GetRepository<ImageModel>().GetAll().ToList();
            if (imageShow)
            {
                foreach (var image in imageList)
                {
                    var imageDto = ObjectMapper.Map<ImageDto>(image);
                    var imageMongo = GetImageMongo(imageDto.Id);
                    if (imageMongo != null)
                    {

                        imageDto.LargeImage = imageMongo.LargeImage;
                        imageDto.SmallImage = imageMongo.SmallImage;
                    }

                    imageDtoList.Add(imageDto);
                }
            }
            else
            {
                imageDtoList.AddRange(from image in imageList select ObjectMapper.Map<ImageDto>(image));
            }
            return imageDtoList;
        }

        public ImageDto GetImage(int id)
        {
            throw new NotImplementedException();
        }

        public ImageDto GetImage(string routeUrl)
        {
            throw new NotImplementedException();
        }

        public List<ImageDto> GetImageList(int categoryId)
        {
            throw new NotImplementedException();
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
        private ImageMongoModel GetImageMongo(int imageId)
        {
            using (MongoRepository<ImageMongoModel> mongoRepository = new MongoRepository<ImageMongoModel>())
            {
                return mongoRepository.GetAll(x => x.ParentId == imageId).FirstOrDefault();
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
                imageDto.RouteUrl = $"{imageDto.RouteUrl}-{urlCount+1}";

            return imageDto.RouteUrl;
        }

    }
}
