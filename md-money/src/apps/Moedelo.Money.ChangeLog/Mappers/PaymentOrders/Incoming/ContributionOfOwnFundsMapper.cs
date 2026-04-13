using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class ContributionOfOwnFundsMapper
    {
        internal static ContributionOfOwnFundsStateDefinition.State MapToState(
            this ContributionOfOwnFundsCreatedMessage eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description
            };
        }

        internal static ContributionOfOwnFundsStateDefinition.State MapToState(
            this ContributionOfOwnFundsUpdatedMessage eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description
            };
        }

        internal static ContributionOfOwnFundsStateDefinition.State MapToState(
            this ContributionOfOwnFundsDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
