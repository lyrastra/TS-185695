namespace Moedelo.CommonV2.EventBus.Billing
{
    public class TransferToBackofficeBillingEvent
    {
        public int PaymentHistoryId { get; set; }
        public string Login { get; set; }
    }
}