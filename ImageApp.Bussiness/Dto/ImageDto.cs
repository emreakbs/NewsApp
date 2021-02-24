using ImageApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Dto
{
    public class ImageDto : ImageModel
    {
        public string LargeImage { get; set; }
        public string SmallImage { get; set; }
    }
}
