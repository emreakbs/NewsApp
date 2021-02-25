using ImageApp.Bussiness.Dto;
using ImageApp.Bussiness.Extension;
using ImageApp.Bussiness.Helper;
using ImageApp.Bussiness.Service.Image;
using ImageApp.Core.Model;
using ImageApp.Data;
using ImageApp.Data.Enum;
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
            imageDto.Id = imageModel.Id;
            var mongoResult = false;
            if (efResult) mongoResult = AddImageMongo(imageDto);

            return mongoResult;
        }
        /// <summary>
        /// Tüm içerikleri döner
        /// </summary>
        /// <param name="imageShow"></param>
        /// <returns></returns>
        public List<ImageDto> GetAllImage()
        {
            using var uow = new UnitOfWork<MasterContext>();
            var imageList = uow.GetRepository<ImageModel>().GetAll().OrderByDescending(o => o.Id).ToList();
            var imageDtoList = ImageModelListToImageDtoList(imageList);
            return imageDtoList;
        }
        /// <summary>
        /// id'ye göre tekli içerik döner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ImageDto GetImage(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// RouteUrl'e göre tekli içerik döner
        /// </summary>
        /// <param name="routeUrl"></param>
        /// <returns></returns>
        public ImageDto GetImage(string routeUrl)
        {
            using var uow = new UnitOfWork<MasterContext>();
            var imageModel = uow.GetRepository<ImageModel>().GetAll(x => x.RouteUrl.Equals(routeUrl)).FirstOrDefault();
            if (imageModel == null) return null;

            var imageMongo = GetImageMongo(imageModel.Id);

            var imageDto = ObjectMapper.Map<ImageDto>(imageModel);
            imageDto.Content = imageMongo.Content;
            imageDto.LargeImage = imageMongo.LargeImage;
            imageDto.SmallImage = imageMongo.SmallImage;

            return imageDto;
        }
        /// <summary>
        /// Paging yaparak kategoriye göre içerik döner
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ImageDto> GetImageList(int categoryId, int page = 0, int count = 4)
        {
            var skipCount = page == 1 ? 0 : page * count - count;
            var takeCount = page * count;
            using var uow = new UnitOfWork<MasterContext>();
            var imageList = uow.GetRepository<ImageModel>().GetDataPart(y => y.CategoryId == categoryId, y => y.CategoryId == categoryId, SortTypeEnum.ASC, skipCount, takeCount).ToList();
            var imageDtoList = ImageModelListToImageDtoList(imageList);
            return imageDtoList;
        }
        /// <summary>
        /// İçerik silme servisi
        /// </summary>
        /// <param name="id">içerik id'si</param>
        /// <returns></returns>
        public bool DeleteImage(int id)
        {
            var response = 0;
            using var uow = new UnitOfWork<MasterContext>();
            var mongoDelete = DeleteImageMongo(id);
            if (mongoDelete)
            {
                uow.GetRepository<ImageModel>().Delete(x => x.Id == id);
                response = uow.SaveChanges();
            }
            return response > 0;
        }

        #region Private Methods
        /// <summary>
        /// MongoDB'den içerik silmeye yarar
        /// </summary>
        /// <param name="id">içerik id</param>
        /// <returns></returns>
        private bool DeleteImageMongo(int id)
        {
            using (MongoRepository<ImageMongoModel> mongoRepository = new MongoRepository<ImageMongoModel>())
            {
                return mongoRepository.Delete(x => x.ParentId == id && x.DatabaseName.Equals("ImageApp") && x.TableName.Equals("Images"));
            }
        }
        /// <summary>
        /// Mongo'ya büyük ve küçük resmi ekler
        /// </summary>
        /// <param name="imageDto">içerik modeli</param>
        /// <param name="largeImage">Büyk resim</param>
        /// <param name="smallImage">Küçk resim</param>
        private bool AddImageMongo(ImageDto imageDto)
        {
            using (MongoRepository<ImageMongoModel> mongoRepository = new MongoRepository<ImageMongoModel>())
            {
                var imageMongoModel = new ImageMongoModel
                {
                    DatabaseName = "ImageApp",
                    TableName = "Images",
                    ParentId = imageDto.Id,
                    Content = imageDto.Content,
                    LargeImage = imageDto.LargeImage,
                    SmallImage = imageDto.SmallImage
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
        /// url'in daha önceden eklenip eklenmediğini recursive olarak kontrol eder
        /// </summary>
        /// <param name="uow">UnitOfWork</param>
        /// <param name="imageDto">İçerik</param>
        /// <returns>Kullanılabilecek URL</returns>
        private string UrlControl(UnitOfWork<MasterContext> uow, ImageDto imageDto, int count = 0)
        {
            if (count == 0)
            {
                var urlCount = uow.GetRepository<ImageModel>().Count(x => x.RouteUrl.Equals(imageDto.RouteUrl));
                if (urlCount > 0) UrlControl(uow, imageDto, 1);
            }
            else
            {
                var controlString = $"{imageDto.RouteUrl}-{count++}";
                var urlCount = uow.GetRepository<ImageModel>().Count(x => x.RouteUrl.Equals(controlString));
                if (urlCount > 0) UrlControl(uow, imageDto, count);
                else imageDto.RouteUrl = controlString;
            }

            return imageDto.RouteUrl;
        }
        /// <summary>
        /// Model listesini Dto Listesine çevirir
        /// </summary>
        /// <param name="imageModelList"></param>
        /// <returns></returns>
        private List<ImageDto> ImageModelListToImageDtoList(List<ImageModel> imageModelList)
        {
            List<ImageDto> imageDtoList = new List<ImageDto>();

            foreach (var image in imageModelList)
            {
                var imageDto = ObjectMapper.Map<ImageDto>(image);
                var imageMongo = GetImageMongo(imageDto.Id);
                if (imageMongo != null)
                {
                    imageDto.LargeImage = imageMongo.LargeImage;
                    imageDto.SmallImage = imageMongo.SmallImage;
                    imageDto.Content = imageMongo.Content;
                }
                imageDtoList.Add(imageDto);
            }

            return imageDtoList;
        }
        #endregion

    }
}
