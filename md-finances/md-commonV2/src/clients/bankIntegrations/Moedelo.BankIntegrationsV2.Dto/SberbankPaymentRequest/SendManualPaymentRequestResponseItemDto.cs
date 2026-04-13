namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SendManualPaymentRequestResponseItemDto
    {
        public int FirmId { get; set; }
        public SendPaymentRequestResponseDto Result { get; set; }
    }
}