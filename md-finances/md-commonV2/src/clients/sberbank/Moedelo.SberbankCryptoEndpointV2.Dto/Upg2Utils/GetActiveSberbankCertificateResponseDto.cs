using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.Upg2Utils
{
    public class GetActiveSberbankCertificateResponseDto
    {
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}