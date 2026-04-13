using System;
using Moedelo.BankIntegrations.Dto;
using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SdmBank
{
    public class TokenDto 
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "scopes")]
        public string[] Scope { get; set; }
        [JsonProperty(PropertyName = "sessionExpirationTime")]
        public int SessionExpirationTime { get; set; }
        [JsonProperty(PropertyName = "time_stamp")]
        public DateTime TimeStamp { get; set; }
    }
}