using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Sso
{
    public class AccessTokenRequestDto
    {
        /// <summary>
        /// Это время мы должны передавать на вызывающей стороне,
        /// чтобы рассинхронизация по времени между серверами не учитывалась
        /// </summary>
        public DateTime RequetStartDate { get; set; }

        public string GrantType { get; set; }

        public string AuthorizationCode { get; set; }

        public string RedirectUri { get; set; }

        public string SberbankGetTokenUri { get; set; }
    }
}