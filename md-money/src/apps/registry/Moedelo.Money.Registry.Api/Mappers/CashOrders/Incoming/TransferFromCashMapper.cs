using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming
{
    internal static class TransferFromCashMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this TransferFromCashCreated eventData)
        {
            return new CreateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingFromAnotherCash,
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
            this TransferFromCashUpdated eventData)
        {
            return new UpdateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingFromAnotherCash,
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
            this TransferFromCashDeleted eventData)
        {
            return new DeleteMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
