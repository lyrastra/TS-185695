using System;
using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class PaymentsWithExForFirmsRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}