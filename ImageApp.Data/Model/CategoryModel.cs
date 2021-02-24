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
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
        public int? ParentCategory { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUser { get; set; }
        public int UpdateUser { get; set; }

        public UserModel UserModel { get; set; }
    }
}
