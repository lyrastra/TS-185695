using System;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SyncAdvanceAcceptancesRequestDto
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}