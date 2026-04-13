using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PurseOperations.Incoming
{
    internal static class PaymentFromCustomerMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this PaymentFromCustomerCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationIncomingPaymentFromCustomer,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.PurseId,
                    Type = OperationSource.Purse
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this PaymentFromCustomerUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationIncomingPaymentFromCustomer,
                Direction = MoneyDirection.Incoming,
                Source = new Source
                {
                    Id = eventData.PurseId,
                    Type = OperationSource.Purse
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = true
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this PaymentFromCustomerDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PurseOperationIncomingPaymentFromCustomer
            };
        }
    }
}
