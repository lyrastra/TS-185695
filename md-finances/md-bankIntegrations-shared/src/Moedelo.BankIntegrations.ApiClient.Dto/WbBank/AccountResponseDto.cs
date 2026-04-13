using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

public class AccountResponseDto
{
    [JsonProperty("account_number")]
    public string AccountNumber { get; set; }

    [JsonProperty("account_type")]
    public string AccountType { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("bank")]
    public BankResponseDto Bank { get; set; }
}
