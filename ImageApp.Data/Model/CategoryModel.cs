using ImageApp.Core.Model;
using ImageApp.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageApp.Data.Model
{
    /// <summary>
    /// Katrgorileri Modeli
    /// </summary>
    [Table("Category")]
    public class CategoryModel : BaseModel
    {
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
        public int? ParentCategory { get; set; }

        public UserModel UserModel { get; set; }

        public ICollection<ImageModel> Images { get; set; }

    }
}
