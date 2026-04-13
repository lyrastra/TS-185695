using System;

namespace Moedelo.CommonV2.EventBus.Billing
{
    public class ModifiedPaymentEvent
    {
        public int PaymentId { get; set; }
        
        public int FirmId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public decimal? Sum { get; set; }

        public string Tariff { get; set; }

        public bool? IsReselling { get; set; }
    }
}
