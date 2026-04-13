namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class PreviewSendPaymentDto
    {
        public string FoundBankBik { get; set; }
        public string FoundInn { get; set; }
        public string OrgHashId { get; set; }
        public int? FirmId { get; set; }
        public string Login { get; set; }
        public string NextPriceListName { get; set; }
        public int NextPriceListId { get; set; }
        public int NextMonthCount { get; set; }
        public decimal NextPrice { get; set; }
        public string CurrentPriceListName { get; set; }
        public int CurrentPriceListId { get; set; }
        public int CurrentMonthCount { get; set; }
        public string AcceptancePurpose { get; set; }
        public string AcceptanceSendPaymentStep { get; set; }
    }
}