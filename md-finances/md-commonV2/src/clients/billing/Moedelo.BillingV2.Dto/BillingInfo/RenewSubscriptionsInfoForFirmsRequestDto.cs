using System;
using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class RenewSubscriptionsInfoForFirmsRequestDto
    {
        public DateTime ForDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IReadOnlyCollection<int> FirmIds { get; set; }

        public IReadOnlyCollection<int> PriceListIds { get; set; }
    }
}