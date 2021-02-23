using ImageApp.Bussiness.Extension;
using ImageApp.Bussiness.Service.Login;
using ImageApp.Core.Dto;
using ImageApp.Core.Model;
using ImageApp.Data;
using ImageApp.Data.Model;
using ImageApp.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageApp.Bussiness.Service
{

    public class LoginService:ILoginService
    {
        #region Single Section

        private static readonly Lazy<LoginService> instance = new Lazy<LoginService>(() => new LoginService());
        public LoginService() { }
        public static LoginService Instance => instance.Value;

        #endregion
        /// <summary>
        /// Kullanıcı oluşturmaya servisi
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Create(UserModel user)
        {
            using var uow = new UnitOfWork<MasterContext>();
            uow.GetRepository<UserModel>().Add(user);
            var result = uow.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// Kullanıcı giriş servisi
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public Token Login(UserModel userModel)
        {
            using var uow = new UnitOfWork<MasterContext>();

            var user = uow.GetRepository<UserModel>()
                .GetAll(x => x.UserName.Equals(userModel.UserName, StringComparison.OrdinalIgnoreCase)
                && x.Password.Equals(CryptoExtensions.Encrypt(userModel.Password))).FirstOrDefault();

            if (user != null)
            {
                //Token üretiliyor.
                var token = TokenHandler.Instance.CreateAccessToken(user);
                token.UserTokenDto = ObjectMapper.Map<UserTokenDto>(user);

                //Refresh token Users tablosuna işleniyor.
                user.RefreshToken = token.RefreshToken;
                user.AccessToken = token.AccessToken;
                user.RefreshTokenEndDate = token.Expiration;

                uow.GetRepository<UserModel>().Update(user);
                uow.SaveChanges();

                return token;
            }
            return null;
        }
        /// <summary>
        /// Token geçerliliğini kontrol eder
        /// </summary>
        /// <param name="id">Kullanıcı Id'si</param>
        /// <returns>Boolean</returns>
        public bool TokenValidate(int id, string token)
        {
            if (id == 0 || string.IsNullOrEmpty(token)) return false;

            using var uow = new UnitOfWork<MasterContext>();

            var user = uow.GetRepository<UserModel>().GetAll(x => x.Id == id).FirstOrDefault();

            if (user.AccessToken != token /*|| user.RefreshTokenEndDate < DateTime.UtcNow*/) return false;

            return true;
        }
        /// <summary>
        /// Kullanıcı oturumunu sonlandırma methodu
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        public bool Logout(int id)
        {
            if (id == 0) return false;

            using var uow = new UnitOfWork<MasterContext>();
            var user = uow.GetRepository<UserModel>().GetAll(x => x.Id == id).FirstOrDefault();
            user.RefreshToken = "";
            user.RefreshTokenEndDate = DateTime.UtcNow;
            user.AccessToken = "";

            uow.GetRepository<UserModel>().Update(user);
            var result = uow.SaveChanges();

            return result > 0;

        }

    }
}
