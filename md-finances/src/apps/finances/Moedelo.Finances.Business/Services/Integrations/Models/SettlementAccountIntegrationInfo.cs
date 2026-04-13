using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.Integrations.Models
{
    internal class SettlementAccountIntegrationInfo
    {
        public SettlementAccountDto SettlementAccount { get; private set; }
        public BankDto Bank { get; private set; }
        public IntegrationPartners IntegrationPartner { get; private set; }
        
        public static SettlementAccountIntegrationInfo CreateFromContext(int settlementAccountId, SendPaymentOrdersContext parentContext)
        {
            var result = new SettlementAccountIntegrationInfo();

            result.SettlementAccount = parentContext.SettlementAccounts[settlementAccountId];
            result.Bank = parentContext.Banks[result.SettlementAccount.BankId];
            result.IntegrationPartner = result.Bank.IntegratedPartner ?? IntegrationPartners.Undefined;
            
            if (parentContext.Integrations.TryGetValue(result.SettlementAccount.Number, out var integration) 
                && result.Bank.Bik == integration.Bik)
            {
                result.IntegrationPartner = integration.IntegrationPartner;
            }
            
            return result;
        }
    }
}