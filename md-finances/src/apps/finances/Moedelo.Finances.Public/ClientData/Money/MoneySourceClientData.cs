using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Public.ClientData
{
    public class MoneySourceClientData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MoneySourceType Type { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
        public string Number { get; set; }

        public bool IsActive { get; set; }
        public bool IsPrimary { get; set; }
        
        public bool IsTransit { get; set; }

        public IntegrationPartners? IntegrationPartner { get; set; }
        public bool HasAvaliableIntegration { get; set; }
        public bool HasActiveIntegration { get; set; }
        public bool CanRequestMovementList { get; set; }
        public bool CanSendPaymentOrder { get; set; }
        public bool HasUnprocessedRequests { get; set; }
        public bool CanSendBankInvoice { get; set; }
        public bool IsReconciliationProcessing { get; set; }
        public string IntegrationImage { get; set; }
        public bool HasEmployees { get; set; }
        public bool HideBalance { get; set; }
        public string BikBank { get; set; }
        public string IconUrl { get; set; }
        public string BankName { get; set; }
    }
}