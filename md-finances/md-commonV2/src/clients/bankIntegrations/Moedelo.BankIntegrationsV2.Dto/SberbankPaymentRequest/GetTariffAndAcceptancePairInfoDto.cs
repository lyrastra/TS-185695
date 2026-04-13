namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class GetTariffAndAcceptancePairInfoDto
    {
        public string ProductName { get; set; }
        public string Link { get; set; }
        public bool IsValid { get; set; }
    }
}
