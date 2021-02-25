using ImageApp.Bussiness.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Service.Image
{
    public interface IImageService
    {
        bool AddImage(ImageDto imageDto, int userId);
        ImageDto GetImage(int id);
        ImageDto GetImage(string routeUrl);
        List<ImageDto> GetAllImage();
        List<ImageDto> GetImageList(int categoryId,int page, int count);
    }
}
