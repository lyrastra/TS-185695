namespace Moedelo.SberbankCryptoEndpointV2.Dto.Sso
{
    public class AccessTokenResponseDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }

        public string IdToken { get; set; }

        public string Error { get; set; }
    }
}