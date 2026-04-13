
namespace Moedelo.BankIntegrations.ApiClient.Dto.Common
{
    public class OpenIdGetTokenResponseDto
    {
        public string IdToken { get; set; }
        
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }
        
        public int ExpiresIn { get; set; }
    }
}
