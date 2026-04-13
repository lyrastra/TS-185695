using Newtonsoft.Json;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptStatusDto
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}