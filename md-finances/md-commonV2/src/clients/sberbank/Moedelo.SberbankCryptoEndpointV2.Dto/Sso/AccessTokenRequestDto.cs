namespace Moedelo.SberbankCryptoEndpointV2.Dto.Sso
{
    public class AccessTokenRequestDto
    {
        /// <summary> Тип доступа </summary>
        public string GrantType { get; set; }

        public string AuthorizationCode { get; set; }

        public long ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }
    }
}