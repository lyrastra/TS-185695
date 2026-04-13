namespace Moedelo.BillingV2.Dto.Billing
{
    public class SwitchIsRefundStateRequestDto
    {
        public int PaymentHistoryId { get; set; }

        public bool IsRefund { get; set; }
    }
}