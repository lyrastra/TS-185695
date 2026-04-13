namespace Moedelo.BillingV2.Dto.Billing
{
    public class SwitchOnPaymentRequestDto
    {
        public PaymentHistoryDto PaymenHistory { get; set; }
        public bool TruncateCurrentPaymentAnyway { get; set; }
        public int UserId { get; set; }
    }
}