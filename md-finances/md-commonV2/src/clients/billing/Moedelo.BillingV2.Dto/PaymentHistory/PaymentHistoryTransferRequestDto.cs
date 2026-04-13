namespace Moedelo.BillingV2.Dto.PaymentHistory
{
    public class PaymentHistoryTransferRequestDto
    {
        public int PartnerUserId { get; set; }
        public int FromFirmId { get; set; }
        public int ToFirmId { get; set; }
    }
}