using ImageApp.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageApp.Data.Model
{
    [Table("Images")]
    public class ImageModel : BaseModel
    {
        /// <summary>
        /// Başlık
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Seo Başlık
        /// </summary>
        public string SeoTitle { get; set; }
        /// <summary>
        /// Seo anahtar kelimeler
        /// </summary>
        public string SeoKeyWord { get; set; }
        /// <summary>
        /// url adresinden çağımak için
        /// </summary>
        public string RouteUrl { get; set; }
        /// <summary>
        /// Hangi kategoride olduğunu belirtir.
        /// </summary>
        public int CategoryId { get; set; }

        public CategoryModel CategoryModel { get; set; }


    }
}
