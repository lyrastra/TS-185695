using System;

namespace Moedelo.CommonV2.EventBus.Account
{
    public class BillingBlTarifChangedEvent
    {
        public int PaymentId { get; set; }

        public int FirmId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
