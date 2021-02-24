using ImageApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageApp.Core.Model
{
    /// <summary>
    /// tüm tablolarda olan fialdlar
    /// </summary>
    public class BaseModel : IBaseModel
    {
        [Key]
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// oluşturulma tarihi
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// düzenlenme tarihi
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// oluşturan kullanıcı
        /// </summary>
        public int CreateUser { get; set; }
        /// <summary>
        /// düzenleyen kullanıcı
        /// </summary>
        public int UpdateUser { get; set; }
        /// <summary>
        /// aktiflik durumu
        /// </summary>
        public bool Status { get; set; }

    }
}
