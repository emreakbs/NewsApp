using ImageApp.Bussiness.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Service.Home
{
    public interface IHomeService
    {
        Dictionary<string, List<ImageDto>> GetImages(int categoryCount,int imageCount);
    }
}
