using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Events;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class LoanIssueMapper
    {
        internal static CreateMoneyOperationCommand MapToCommand(
            this LoanIssueCreated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingLoanIssue,
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
            this LoanIssueUpdated eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingLoanIssue,
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
            this LoanIssueDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                OperationType = OperationType.PaymentOrderOutgoingLoanIssue
            };
        }
    }
}
