using System.Collections.Generic;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response
{
    public class GetSberbankPaymentRequestsStatusResponseDto
    {
        public List<SberbankPaymentRequestStatusResponseDto> ResultList { get; set; }
    }
}