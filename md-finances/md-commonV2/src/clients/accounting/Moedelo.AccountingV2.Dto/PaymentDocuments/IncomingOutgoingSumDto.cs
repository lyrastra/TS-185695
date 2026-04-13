namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class IncomingOutgoingSumDto
    {
        public int KontragentId { get; set; }
        public decimal IncomingSum { get; set; }
        public decimal OutgoingSum { get; set; }
    }
}
