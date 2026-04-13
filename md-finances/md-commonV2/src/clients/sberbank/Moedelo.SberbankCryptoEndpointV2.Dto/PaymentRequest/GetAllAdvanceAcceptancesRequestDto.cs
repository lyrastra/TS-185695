using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest
{
    public class GetAllAdvanceAcceptancesRequestDto
    {
        public string ClientId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}