using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Sso
{
    public class AccessTokenResponseDto
    {
        public string AccessToken { get; set; }

        public DateTime? AccessTokenExpirationDate { get; set; }

        public string TokenType { get; set; }

        public string IdToken { get; set; }

        public string Error { get; set; }
    }
}