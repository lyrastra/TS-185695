namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class SberbankIntegrationDataDto
    {
        public int FirmId { get; set; }
        public int NextPriceListId { get; set; }
        public string ClientId { get; set; }
        public bool HasPaymentRequestError { get; set; }
        public string ExternalClientId { get; set; }
        public bool IsAccountBlocked { get; set; }
        public bool IsAccountClosed { get; set; }
        public string AuthorizationType { get; set; }
    }
}
