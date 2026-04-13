using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PurseOperations.Outgoing
{
    internal static class WithholdingOfFeeMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this WithholdingOfFeeCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationComission,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.PurseId,
                    Type = OperationSource.Purse
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this WithholdingOfFeeUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationComission,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.PurseId,
                    Type = OperationSource.Purse
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this WithholdingOfFeeDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationComission
            };
        }
    }
}
