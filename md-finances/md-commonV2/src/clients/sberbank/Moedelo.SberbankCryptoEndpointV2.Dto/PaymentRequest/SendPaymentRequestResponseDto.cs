using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest
{
    public class SendPaymentRequestResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
        public Guid ExternalDocumentId { get; set; }
    }
}