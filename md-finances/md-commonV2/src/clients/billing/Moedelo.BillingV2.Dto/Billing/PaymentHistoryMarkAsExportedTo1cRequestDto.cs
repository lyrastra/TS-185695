namespace Moedelo.BillingV2.Dto.Billing
{
    public class PaymentHistoryMarkAsExportedTo1cRequestDto
    {
        public int PartnerUserId { get; set; }
        public int[] PaymentIds { get; set; }
    }
}