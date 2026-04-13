namespace Moedelo.BackofficeV2.Dto.SendBill
{
    public class ResendBillRequestDto
    {
        public int PaymentId { get; set; }
        public string Payer { get; set; }
        public bool IsSendForUser { get; set; }
        public string ToFio { get; set; }
        public string AdditionalSendBillEmail { get; set; }
        public string BillExpirationDate { get; set; }
        public string Note { get; set; }
        public string CoveringMessage { get; set; }
        public int UserSenderId { get; set; }
        public string UserSenderLogin { get; set; }
    }
}
