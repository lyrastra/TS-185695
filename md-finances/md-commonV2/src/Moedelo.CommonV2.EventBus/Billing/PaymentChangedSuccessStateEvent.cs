using System;

namespace Moedelo.CommonV2.EventBus.Billing
{
    public class PaymentChangedSuccessStateEvent
    {
        public int PaymentId { get; set; }
        public int FirmId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
    }
}