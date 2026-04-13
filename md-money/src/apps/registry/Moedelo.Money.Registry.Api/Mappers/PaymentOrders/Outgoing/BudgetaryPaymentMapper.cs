using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class BudgetaryPaymentMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this BudgetaryPaymentCreated eventData)
        {
            return new ()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.BudgetaryPayment,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Sum = eventData.Sum,
                IsPaid = eventData.IsPaid
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this BudgetaryPaymentUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.BudgetaryPayment,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Sum = eventData.Sum,
                IsPaid = eventData.IsPaid
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this BudgetaryPaymentDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.BudgetaryPayment
            };
        }
    }
}
