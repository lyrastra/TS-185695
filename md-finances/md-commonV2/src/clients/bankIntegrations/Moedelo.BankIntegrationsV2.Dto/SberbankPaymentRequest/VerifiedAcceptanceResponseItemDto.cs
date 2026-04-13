namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class VerifiedAcceptanceResponseItemDto
    {
        public int FirmId { get; set; }

        /// <summary> Текст результата (при ошибке) после проверки согласия на ЗДА </summary>
        public string Message { get; set; }
    }
}