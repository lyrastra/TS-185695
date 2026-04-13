using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl
{
    public class ClientInfoRequestDto
    {
        [JsonProperty("client_hash_id")]
        public string ClientHashId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}