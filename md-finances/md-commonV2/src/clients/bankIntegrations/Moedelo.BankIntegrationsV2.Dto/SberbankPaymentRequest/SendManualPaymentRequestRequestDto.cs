using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SendManualPaymentRequestRequestDto
    {
        public List<int> FirmIds { get; set; }
    }
}