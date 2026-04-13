using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class RenewSubscriptionsCountSummaryForFirmsResponseDto
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int AllCount { get; set; }

        public int RenewedCount { get; set; }
    }
}