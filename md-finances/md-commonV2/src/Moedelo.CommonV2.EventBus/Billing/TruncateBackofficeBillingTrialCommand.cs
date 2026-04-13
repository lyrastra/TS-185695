using System;

namespace Moedelo.CommonV2.EventBus.Billing
{
    public class TruncateBackofficeBillingTrialCommand
    {
        public int PaymentHistoiryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
