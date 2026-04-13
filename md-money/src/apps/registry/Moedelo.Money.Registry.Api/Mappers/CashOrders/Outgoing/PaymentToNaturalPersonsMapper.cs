using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.CashOrders.Outgoing
{
    internal static class PaymentToNaturalPersonsMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this PaymentToNaturalPersonsCreated eventData)
        {
            return new CreateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderOutgoingPaymentForWorking,
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
            this PaymentToNaturalPersonsUpdated eventData)
        {
            return new UpdateMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderOutgoingPaymentForWorking,
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
            this PaymentToNaturalPersonsDeleted eventData)
        {
            return new DeleteMoneyOperationCommand
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.CashOrderOutgoingPaymentForWorking
            };
        }
    }
}
