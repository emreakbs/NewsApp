using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Core.Interface
{
    public interface IBaseModel
    {
        int Id { get; set; }
        /// <summary>
        /// oluşturulma tarihi
        /// </summary>
        DateTime CreateDate { get; set; }
        /// <summary>
        /// düzenlenme tarihi
        /// </summary>
        DateTime UpdateDate { get; set; }
        /// <summary>
        /// oluşturan kullanıcı
        /// </summary>
        int CreateUser { get; set; }
        /// <summary>
        /// düzenleyen kullanıcı
        /// </summary>
        int UpdateUser { get; set; }
        /// <summary>
        /// aktiflik durumu
        /// </summary>
        bool Status { get; set; }
    }
}
