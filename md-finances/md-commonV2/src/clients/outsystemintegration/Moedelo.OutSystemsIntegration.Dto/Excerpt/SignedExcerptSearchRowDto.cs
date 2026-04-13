using System;
using Newtonsoft.Json;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptSearchRowDto : IHaveToken
    {
        [JsonProperty("a")]        
        public string Address { get; set; }
        
        [JsonProperty("c")]
        public string ShortName { get; set; }
        
        [JsonProperty("g")]
        public string Director { get; set; }

        [JsonProperty("cnt")]
        public int Count { get; set; }
        
        [JsonProperty("i")]
        public string Inn { get; set; }
        
        [JsonProperty("k")]
        public string Type { get; set; }

        [JsonProperty("n")]
        public string FullName { get; set; }

        [JsonProperty("o")]
        public string Ogrn { get; set; }

        [JsonProperty("p")]
        public string Kpp { get; set; }

        [JsonProperty("r")]
        public DateTime? RegistrationDate { get; set; }
                
        [JsonProperty("t")]
        public string Token { get; set; }

        [JsonProperty("pg")]
        public int Page { get; set; }
        
        [JsonProperty("tot")]
        public int Total { get; set; }
    }
}