using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Incoming
{
    internal static class ContributionOfOwnFundsMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this ContributionOfOwnFundsCreatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingContributionOfOwnFunds,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this ContributionOfOwnFundsUpdatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingContributionOfOwnFunds,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this ContributionOfOwnFundsDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingContributionOfOwnFunds
            };
        }
    }
}
