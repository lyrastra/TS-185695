using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyTransferFromAccountMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this CurrencyTransferFromAccountCreatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingCurrencyTransferFromAccount,
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
            this CurrencyTransferFromAccountUpdatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingCurrencyTransferFromAccount,
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
            this CurrencyTransferFromAccountDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingCurrencyTransferFromAccount
            };
        }
    }
}
