using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Incoming
{
    internal static class TransferFromPurseMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this TransferFromPurseCreatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingTransferFromPurse,
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
            this TransferFromPurseUpdatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingTransferFromPurse,
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
            this TransferFromPurseDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingTransferFromPurse
            };
        }
    }
}
