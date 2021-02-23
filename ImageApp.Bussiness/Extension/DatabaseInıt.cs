using ImageApp.Data;
using ImageApp.Data.Model;
using ImageApp.DataAccess.UnitOfWork;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Bussiness.Extension
{
    public class DatabaseInit
    {
        #region Single Section

        private static readonly Lazy<DatabaseInit> instance = new Lazy<DatabaseInit>(() => new DatabaseInit());
        public DatabaseInit() { }
        public static DatabaseInit Instance => instance.Value;

        #endregion
        /// <summary>
        /// Defaul kullanıcı database'de var mı kontrol eder.
        /// Eğer yoksa default olarak kullanıcı ekler.
        /// Varsa her hangi bir işlem yapılmaz.
        /// </summary>
        public void UserControl()
        {
            using var uow = new UnitOfWork<MasterContext>();
            var userCount = uow.GetRepository<UserModel>().Count(x => x.UserName == "root" && x.Password == CryptoExtensions.Encrypt("root"));
            if (userCount < 1) SetFirstUser();
        }
        /// <summary>
        /// İlk kullanıcıyı ekleme işlemini yapar
        /// </summary>
        private void SetFirstUser()
        {
            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<UserModel>().Add(new UserModel
            {
                FirstName = "Admin",
                LastName = "Admin",
                Password = CryptoExtensions.Encrypt("root"),
                UserName = "root"
            });
            uow.SaveChanges();
        }
    }
}
