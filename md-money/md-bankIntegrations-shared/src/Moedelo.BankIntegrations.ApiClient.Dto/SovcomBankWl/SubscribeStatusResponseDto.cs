using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl
{
    public class SubscribeStatusResponseDto
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("client_hash_id")]
        public string ClientHashId { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_success")]
        public bool IsSuccess { get; set; }
    }
}
