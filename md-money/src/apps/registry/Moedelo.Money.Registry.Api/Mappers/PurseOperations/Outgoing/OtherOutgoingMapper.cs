using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PurseOperations.Outgoing
{
    internal static class OtherOutgoingMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this OtherOutgoingCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationOtherOutgoing,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.PurseId,
                    Type = OperationSource.Purse
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this OtherOutgoingUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationOtherOutgoing,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.PurseId,
                    Type = OperationSource.Purse
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this OtherOutgoingDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationOtherOutgoing
            };
        }
    }
}
