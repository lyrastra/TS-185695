using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PurseOperations.Outgoing
{
    internal static class TransferToSettlementAccountMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this TransferToSettlementAccountCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationTransferToSettlement,
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
            this TransferToSettlementAccountUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationTransferToSettlement,
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
            this TransferToSettlementAccountDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationTransferToSettlement
            };
        }
    }
}
