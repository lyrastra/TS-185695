using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl
{
    public class AccountResponseDto
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("bic")]
        public string Bic { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }
}