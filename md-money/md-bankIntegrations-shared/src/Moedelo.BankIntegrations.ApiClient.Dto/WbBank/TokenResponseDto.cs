namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

public class TokenResponseDto
{
    public string AccessToken { get; set; }

    public string TokenType { get; set; }

    public int ExpiresIn { get; set; }

    public string RefreshToken { get; set; }

    public int? RefreshExpiresIn { get; set; }

    public string SessionState { get; set; }

    public string Error { get; set; }

    public string ErrorDescription { get; set; }
}
