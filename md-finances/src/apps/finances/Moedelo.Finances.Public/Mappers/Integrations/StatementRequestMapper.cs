using System;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Public.ClientData.Integrations;

namespace Moedelo.Finances.Public.Mappers.Integrations
{
    public static class StatementRequestMapper
    {
        public static BankStatementRequestBySettlementAccount MapBankStatementRequest(StatementRequestBySourceClientData clientData)
        {
            if (clientData.SourceType != MoneySourceType.SettlementAccount)
            {
                throw new NotSupportedException(
                    $"Expected settlement account, but got {clientData.SourceType.ToString()}");
            }
            return new BankStatementRequestBySettlementAccount
            {
                StartDate = clientData.StartDate,
                EndDate = clientData.EndDate,
                SettlementAccountId = (int)clientData.SourceId
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