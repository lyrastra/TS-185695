using System;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SberbankPaymentRequestStatusItem
    {
        public Guid RequestId { get; set; }
        public int FirmId { get; set; }
    }
}
