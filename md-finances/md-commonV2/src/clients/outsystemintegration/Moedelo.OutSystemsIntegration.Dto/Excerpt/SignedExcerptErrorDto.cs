using Newtonsoft.Json;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptErrorDto
    {
        [JsonProperty("captchaSearch")]
        public string[] CaptchaSearch { get; set; }
    }
}