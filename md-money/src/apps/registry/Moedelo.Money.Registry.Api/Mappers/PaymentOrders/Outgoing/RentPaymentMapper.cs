using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class RentPaymentMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this RentPaymentCreatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingRentPayment,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = eventData.IsPaid
            };
        }

        internal static UpdateMoneyOperationCommand MapToCommand(
            this RentPaymentUpdatedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingRentPayment,
                Direction = MoneyDirection.Outgoing,
                Source = new Source
                {
                    Id = eventData.SettlementAccountId,
                    Type = OperationSource.SettlementAccount
                },
                Contractor = eventData.Contractor.Map(),
                Sum = eventData.Sum,
                IsPaid = eventData.IsPaid
            };
        }

        internal static DeleteMoneyOperationCommand MapToCommand(
            this RentPaymentDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingRentPayment
            };
        }
    }
}
