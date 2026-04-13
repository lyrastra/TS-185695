namespace Moedelo.BankIntegrations.ApiClient.Dto.Alfa;

public class RequestTokensRequestDto
{
    public string Code { get; set; }
    public string CodeVerifier { get; set; }
    public string RedirectUri { get; set; }
    public string ClientId  {get; set; }
}