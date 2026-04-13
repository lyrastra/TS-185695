using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionOfOwnFunds.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming
{
    internal static class ContributionOfOwnFundsMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this ContributionOfOwnFundsCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingContributionOfOwnFunds,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.CashId,
                    Type = OperationSource.Cashbox
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this ContributionOfOwnFundsUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingContributionOfOwnFunds,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.CashId,
                    Type = OperationSource.Cashbox
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this ContributionOfOwnFundsDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingContributionOfOwnFunds
            };
        }
    }
}
