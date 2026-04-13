using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class FirmsTransitionTypesByPaymentsRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}