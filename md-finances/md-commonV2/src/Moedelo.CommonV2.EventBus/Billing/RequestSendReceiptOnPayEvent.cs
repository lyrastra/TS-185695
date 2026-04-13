using System;

namespace Moedelo.CommonV2.EventBus.Billing
{
    public class RequestSendReceiptOnPayEvent
    {
        public int PaymentId { get; set; }
        public string CustomerNumber { get; set; }
        public string ReceiptConfig { get; set; }
        public DateTime Timestamp { get; set; }
    }
}