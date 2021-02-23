using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageApp.Data.Model
{
    /// <summary>
    /// Kullanıcı tablosunu belirtir.
    /// </summary>
    [Table("Users")]
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = "unnamed";
        public string LastName { get; set; } = "unnamed";
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
