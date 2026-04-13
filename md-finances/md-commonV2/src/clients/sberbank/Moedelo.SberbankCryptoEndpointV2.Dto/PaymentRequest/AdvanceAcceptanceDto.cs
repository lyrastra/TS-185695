namespace Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest
{
    public class AdvanceAcceptanceDto
    {
        public string AcceptLastDate { get; set; }
        public string AcceptStartDate { get; set; }
        public bool Active { get; set; }
        public string PayerAccount { get; set; }
        public string PayerBankBic { get; set; }
        public string PayerInn { get; set; }
        public string PayerName { get; set; }
        public string PayerOrgIdHash { get; set; }
        public string Purpose { get; set; }
        public string SinceDate { get; set; }
        public string UntilDate { get; set; }
    }
}