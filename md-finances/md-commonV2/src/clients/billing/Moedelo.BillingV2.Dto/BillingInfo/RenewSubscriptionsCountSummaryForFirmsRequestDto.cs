using System;
using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class RenewSubscriptionsCountSummaryForFirmsRequestDto
    {
        public DateTime ForDate { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }

        public IReadOnlyCollection<int> PriceListIds { get; set; }
    }
}