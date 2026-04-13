using System;
using Newtonsoft.Json;

namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

/// <summary>
/// Данные токена интеграции.
/// </summary>
public class TokenDataDto
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("token_date")]
    public DateTime TokenDate { get; set; }
}
