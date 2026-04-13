using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming
{
    internal static class TransferFromSettlementAccountMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this TransferFromSettlementAccountCreated eventData)
        {
            return new CreateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingFromSettlementAccount,
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
            this TransferFromSettlementAccountUpdated eventData)
        {
            return new UpdateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderIncomingFromSettlementAccount,
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
            this TransferFromSettlementAccountDeleted eventData)
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
