using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Domain.Models.Money
{
    public class MoneySource : MoneySourceBase
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string BankBik { get; set; }

        /// <summary> Только для расчетных счетов </summary>
        public string Number { get; set; }

        public bool IsActive { get; set; }

        public bool IsTransit { get; set; }

        public IntegrationPartners? IntegrationPartner { get; set; }

        public string IntegrationImage { get; set; }

        public bool HasAvaliableIntegration { get; set; }

        public bool HasActiveIntegration { get; set; }

        public bool CanRequestMovementList { get; set; }

        public bool CanSendPaymentOrder { get; set; }

        public bool HasUnprocessedRequests { get; set; }
        
        public bool CanSendBankInvoice { get; set; }

        public bool IsReconciliationProcessing { get; set; }

        public bool HasEmployees { get; set; }

        /// <summary>
        /// Для сокрытия общей суммы балансов если есть балансы в разных валютах
        /// </summary>
        public bool HideBalance { get; set; }

        public string IconUrl { get; set; }
    }
}