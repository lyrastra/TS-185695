using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class PreviewSendPaymentRequestResponseDto
    {
        public List<int> FirmIds { get; set; }
        public int LastAcceptanceId { get; set; }
    }
}
