using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

public class BankResponseDto
{
    [JsonProperty("correspondentAccount")]
    public string CorrespondentAccount { get; set; }

    [JsonProperty("inn")]
    public string Inn { get; set; }

    [JsonProperty("bic")]
    public string Bic { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("kpp")]
    public string Kpp { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }
}
