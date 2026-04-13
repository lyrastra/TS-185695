using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyPaymentFromCustomerMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this CurrencyPaymentFromCustomerCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this CurrencyPaymentFromCustomerUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this CurrencyPaymentFromCustomerDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer
            };
        }
    }
}
