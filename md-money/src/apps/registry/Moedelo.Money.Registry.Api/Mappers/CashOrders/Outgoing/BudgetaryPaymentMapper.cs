using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.CashOrders.Outgoing
{
    internal static class BudgetaryPaymentMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this BudgetaryPaymentCreated eventData)
        {
            return new CreateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderBudgetaryPayment,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.CashId,
                    Type = OperationSource.Cashbox
                },
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this BudgetaryPaymentUpdated eventData)
        {
            return new UpdateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderBudgetaryPayment,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.CashId,
                    Type = OperationSource.Cashbox
                },
                Sum = eventData.Sum,
                IsPaid = true
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
                OperationType = OperationType.CashOrderBudgetaryPayment
            };
        }
    }
}
