using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class FirmsPayedTypesByPaymentsRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}