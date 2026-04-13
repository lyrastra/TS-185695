using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class LoanIssueMapper
    {
        internal static LoanIssueStateDefinition.State MapToState(
            this LoanIssueCreated eventData,
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
                Description = eventData.Description,
                IsLongTermLoan = eventData.IsLongTermLoan,

                IsPaid = eventData.IsPaid,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static LoanIssueStateDefinition.State MapToState(
            this LoanIssueUpdated eventData,
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
                Description = eventData.Description,
                IsLongTermLoan = eventData.IsLongTermLoan,

                IsPaid = eventData.IsPaid,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static LoanIssueStateDefinition.State MapToState(
            this LoanIssueDeleted eventData)
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
