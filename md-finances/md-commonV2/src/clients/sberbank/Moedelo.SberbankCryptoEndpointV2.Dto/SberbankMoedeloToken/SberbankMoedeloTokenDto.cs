using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.SberbankMoedeloToken
{
    public class SberbankMoedeloTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime? SessionLastDate { get; set; }
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
        public string OrganizationIdHash { get; set; }
    }
}
