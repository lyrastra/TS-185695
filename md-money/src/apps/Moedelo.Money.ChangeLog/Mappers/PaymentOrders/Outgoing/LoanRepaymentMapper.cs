using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class LoanRepaymentMapper
    {
        internal static LoanRepaymentStateDefinition.State MapToState(
            this LoanRepaymentCreated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                LoanInterestSum = eventData.LoanInterestSum.HasValue
                    ? MoneySum.InRubles(eventData.LoanInterestSum.Value)
                    : null,
                Description = eventData.Description,
                IsLongTermLoan = eventData.IsLongTermLoan,

                IsPaid = eventData.IsPaid,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static LoanRepaymentStateDefinition.State MapToState(
            this LoanRepaymentUpdated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                LoanInterestSum = eventData.LoanInterestSum.HasValue
                    ? MoneySum.InRubles(eventData.LoanInterestSum.Value)
                    : null,
                Description = eventData.Description,
                IsLongTermLoan = eventData.IsLongTermLoan,

                IsPaid = eventData.IsPaid,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static LoanRepaymentStateDefinition.State MapToState(
            this LoanRepaymentDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
