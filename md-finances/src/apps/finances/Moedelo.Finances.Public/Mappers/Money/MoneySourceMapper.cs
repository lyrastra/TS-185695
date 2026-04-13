using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moedelo.CommonV2.Webpack.Helper;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Public.ClientData;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneySourceMapper
    {
        public static List<MoneySourceClientData> MapToClient(this IReadOnlyList<MoneySource> sources, string host)
        {
            return sources.Select(src => MapToClient(src, host)).ToList();
        }

        private static MoneySourceClientData MapToClient(MoneySource source, string host)
        {
            return new MoneySourceClientData
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Balance = source.Balance,
                Currency = source.Currency,
                Number = source.Number,
                IsActive = source.IsActive,
                IsPrimary = source.IsPrimary,
                IsTransit = source.IsTransit,
                IntegrationPartner = source.IntegrationPartner,
                IntegrationImage = source.IntegrationImage,
                IconUrl = source.IconUrl == null ? source.IconUrl : WebstaticUrlHelper.GetUrl(source.IconUrl, host),
                HasActiveIntegration = source.HasActiveIntegration,
                HasUnprocessedRequests = source.HasUnprocessedRequests,
                HasAvaliableIntegration = source.HasAvaliableIntegration,
                CanRequestMovementList = source.CanRequestMovementList,
                CanSendPaymentOrder = source.CanSendPaymentOrder,
                CanSendBankInvoice = source.CanSendBankInvoice,
                IsReconciliationProcessing = source.IsReconciliationProcessing,
                HasEmployees = source.HasEmployees,
                HideBalance = source.HideBalance,
                BikBank = source.BankBik,
                BankName = source.BankName
            };
        }
    }
}