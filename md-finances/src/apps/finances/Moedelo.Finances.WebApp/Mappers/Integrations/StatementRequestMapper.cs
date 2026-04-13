using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.WebApp.ClientData.Integrations;

namespace Moedelo.Finances.WebApp.Mappers.Integrations
{
    public static class StatementRequestMapper
    {
        public static BankStatementRequestByIntegrationPartner MapBankStatementRequest(StatementRequestByIntegrationPartnerClientData clientData)
        {
            return new BankStatementRequestByIntegrationPartner
            {
                StartDate = clientData.StartDate,
                EndDate = clientData.EndDate,
                IntegrationPartner = clientData.IntegrationPartner
            };
        }

        public static BankStatementResponseClientData MapBankStatementResponse(BankStatementResponse response)
        {
            return new BankStatementResponseClientData
            {
                IsSuccess = response.IsSuccess,
                Message = response.Message,
                PhoneMask = response.PhoneMask,
                MessageList = response.MessageList
            };
        }
    }
}