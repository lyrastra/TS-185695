using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming
{
    internal static class ContributionToAuthorizedCapitalMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this ContributionToAuthorizedCapitalCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingContributionAuthorizedCapital,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.CashId,
                    Type = OperationSource.Cashbox
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this ContributionToAuthorizedCapitalUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingContributionAuthorizedCapital,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.CashId,
                    Type = OperationSource.Cashbox
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this ContributionToAuthorizedCapitalDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingContributionAuthorizedCapital
            };
        }
    }
}
