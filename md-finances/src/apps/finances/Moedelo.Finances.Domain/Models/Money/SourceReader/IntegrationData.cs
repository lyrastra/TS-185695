using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.Finances.Domain.Models.Money.SourceReader
{
    public class IntegrationData
    {
        public IntegrationPartners IntegrationPartner { get; set; }
        public bool HasActiveIntegration { get; set; }
        public bool CanRequestMovementList { get; set; }
        public bool CanSendPaymentOrder { get; set; }
        public bool HasUnprocessedRequests { get; set; }
        public string IntegrationImage { get; set; }
        public bool CanSendBankInvoice { get; set; }
    }
}
