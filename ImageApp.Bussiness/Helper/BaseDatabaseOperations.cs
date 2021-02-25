using ImageApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Helper
{
    public class BaseDatabaseOperations
    {

        #region Single Section

        private static readonly Lazy<BaseDatabaseOperations> instance = new Lazy<BaseDatabaseOperations>(() => new BaseDatabaseOperations());
        public BaseDatabaseOperations() { }
        public static BaseDatabaseOperations Instance => instance.Value;

        #endregion

        /// <summary>
        /// Database'e kayıt eklerken default eklenmesi gereken field'ları doldurur
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public T SetCreateValues<T>(T model, int userId) where T : BaseModel
        {
            model.CreateDate = DateTime.UtcNow;
            model.UpdateDate = DateTime.UtcNow;
            model.CreateUser = userId;
            model.UpdateUser = userId;
            return model;
        }

        /// <summary>
        /// Database'de düzenleme yaparken dafault eklenmesi gereken field'ları doldurur.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public T SetUpdateValues<T>(T model, int userId) where T : BaseModel
        {
            model.UpdateDate = DateTime.UtcNow;
            model.UpdateUser = userId;
            return model;
        }

    }
}
