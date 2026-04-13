using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SendManualPaymentRequestResponseDto
    {
        public string Error { get; set; }
        public List<SendManualPaymentRequestResponseItemDto> Data { get; set; }
    }
}