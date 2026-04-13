namespace Moedelo.PayrollV2.Client.ChargePayments.DTO
{
    public class UnbindDocumentRequest
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public long DocumentBaseId { get; set; }
    }
}
