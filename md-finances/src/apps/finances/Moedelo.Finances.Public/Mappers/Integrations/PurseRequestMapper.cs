using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Public.ClientData.Integrations;

namespace Moedelo.Finances.Public.Mappers.Integrations
{
    public static class PurseRequestMapper
    {
        public static StatementRequestByPurse MapPurseRequest(StatementRequestBySourceClientData clientData)
        {
            return new StatementRequestByPurse
            {
                StartDate = clientData.StartDate,
                EndDate = clientData.EndDate,
                KontragentId = clientData.SourceId
            };
        }

        public static BankStatementResponseClientData MapPurseResponse(BankStatementResponse response)
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