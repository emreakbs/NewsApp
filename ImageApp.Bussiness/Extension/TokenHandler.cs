using ImageApp.Core.Model;
using ImageApp.Data.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace ImageApp.Bussiness.Extension
{
    public class TokenHandler
    {

        #region Single Section

        private static readonly Lazy<TokenHandler> instance = new Lazy<TokenHandler>(() => new TokenHandler());
        public TokenHandler() { }
        public static TokenHandler Instance => instance.Value;

        #endregion

        //Token üretecek metot.
        public Token CreateAccessToken(UserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Token tokenInstance = new Token();

            //Security  Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECURITY_KEY")));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            tokenInstance.Expiration = DateTime.UtcNow.AddHours(4);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("TOKEN_ISSUER"),
                audience: Environment.GetEnvironmentVariable("TOKEN_AUDIENCE"),
                expires: tokenInstance.Expiration,//Token süresini 4 saat olarak belirliyorum
                notBefore: DateTime.UtcNow,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                signingCredentials: signingCredentials
                );

            //Token oluşturucu sınıfında bir örnek alıyoruz.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token üretiyoruz.
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            //Refresh Token üretiyoruz.
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;
        }

        //Refresh Token üretecek metot.
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
