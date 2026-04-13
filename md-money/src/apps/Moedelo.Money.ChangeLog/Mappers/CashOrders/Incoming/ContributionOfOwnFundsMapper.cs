using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionOfOwnFunds.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class ContributionOfOwnFundsMapper
    {
        internal static ContributionOfOwnFundsStateDefinition.State MapToState(
            this ContributionOfOwnFundsCreated eventData,
            CashDto cash)
        {
            return new ContributionOfOwnFundsStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment
            };
        }

        internal static ContributionOfOwnFundsStateDefinition.State MapToState(
            this ContributionOfOwnFundsUpdated eventData,
            CashDto cash)
        {
            return new ContributionOfOwnFundsStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingContributionOfOwnFunds
                    ? eventData.OldOperationType.GetDescription()
                    : null
            };
        }

        internal static ContributionOfOwnFundsStateDefinition.State MapToState(
            this ContributionOfOwnFundsDeleted eventData)
        {
            return new ContributionOfOwnFundsStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
            };
        }
    }
}
