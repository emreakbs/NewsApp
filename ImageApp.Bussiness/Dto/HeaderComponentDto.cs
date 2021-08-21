using ImageApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Dto
{
    public class HeaderComponentDto
    {
        public UserTokenDto UserTokenDto { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
    }
}
