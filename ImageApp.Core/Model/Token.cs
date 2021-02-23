using ImageApp.Core.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Core.Model
{
    public class Token
    {

        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("Expiration")]
        public DateTime Expiration { get; set; }
        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }
        [JsonProperty("UserTokenDto")]
        public UserTokenDto UserTokenDto { get; set; }
    }
}
