using System;
using System.Collections.Generic;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest
{
    public class GetPaymentRequestStatusRequestDto
    {
        public string ClientId { get; set; }
        public List<Guid> ExternalDocumentIds { get; set; }
    }
}