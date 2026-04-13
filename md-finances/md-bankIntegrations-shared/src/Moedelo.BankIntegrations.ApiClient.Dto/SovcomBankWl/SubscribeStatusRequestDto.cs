using Newtonsoft.Json;
using Moedelo.BankIntegrations.Enums.SowcombankWl;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl
{
    public class SubscribeStatusRequestDto
    {
        [JsonProperty("client_hash_id")]
        public string ClientHashId { get; set; }

    [JsonProperty("client_status")]
    public ClientStatus ClientStatus { get; set; }
    }
}
