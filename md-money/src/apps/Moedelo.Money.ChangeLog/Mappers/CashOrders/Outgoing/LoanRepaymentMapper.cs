using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.LoanRepayment.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class LoanRepaymentMapper
    {
        internal static LoanRepaymentStateDefinition.State MapToState(
            this LoanRepaymentCreated eventData,
            CashDto cash,
            ContractDto contract)
        {
            return new LoanRepaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                LoanInterestSum = eventData.LoanInterestSum.HasValue
                    ? MoneySum.InRubles(eventData.LoanInterestSum.Value)
                    : null,
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                IsLongTermLoan = eventData.IsLongTermLoan,
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static LoanRepaymentStateDefinition.State MapToState(
            this LoanRepaymentUpdated eventData,
            CashDto cash,
            ContractDto contract)
        {
            return new LoanRepaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                LoanInterestSum = eventData.LoanInterestSum.HasValue
                    ? MoneySum.InRubles(eventData.LoanInterestSum.Value)
                    : null,
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                IsLongTermLoan = eventData.IsLongTermLoan,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingLoanRepayment
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static LoanRepaymentStateDefinition.State MapToState(
            this LoanRepaymentDeleted eventData)
        {
            return new LoanRepaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
