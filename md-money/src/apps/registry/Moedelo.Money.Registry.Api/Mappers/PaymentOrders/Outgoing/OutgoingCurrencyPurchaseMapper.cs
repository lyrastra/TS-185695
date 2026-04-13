using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class OutgoingCurrencyPurchaseMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this OutgoingCurrencyPurchaseCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingCurrencyPurchase,
                Direction = MoneyDirection.Outgoing,
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
            this OutgoingCurrencyPurchaseUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingCurrencyPurchase,
                Direction = MoneyDirection.Outgoing,
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
            this OutgoingCurrencyPurchaseDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingCurrencyPurchase
            };
        }
    }
}
