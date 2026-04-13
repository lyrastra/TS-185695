using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.OtpBank;

public class TokenResponseDto
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public int RefreshExpiresIn { get; set; }
    public string TokenType { get; set; }
    public int NotBeforePolicy { get; set; }
    public string SessionState { get; set; }
    public DateTime CreatedAt { get; set; }
}
