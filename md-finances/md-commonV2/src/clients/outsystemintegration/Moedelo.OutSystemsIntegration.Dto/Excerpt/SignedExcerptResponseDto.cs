using Newtonsoft.Json;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptResponseDto : IHaveToken
    {
        [JsonProperty("t")]
        public string Token { get; set; }

        [JsonProperty("captchaRequired")]
        public bool IsCaptchaRequired { get; set; }
        
        [JsonProperty("ERRORS")]
        public SignedExcerptErrorDto Errors { get; set; }
    }
}