using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Skbbank
{
    public class SkbMovementRequestResponseDto
    {
        [JsonProperty(PropertyName = "statement")]
        public string Statement { get; set; }

        public bool Success => Statement.ToLower() == "requested";
    }
}