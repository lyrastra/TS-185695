using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class LoanObtainingMapper
    {
        internal static LoanObtainingStateDefinition.State MapToState(
            this LoanObtainingCreatedMessage eventData,
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

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static LoanObtainingStateDefinition.State MapToState(
            this LoanObtainingUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsLongTermLoan = eventData.IsLongTermLoan,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static LoanObtainingStateDefinition.State MapToState(
            this LoanObtainingDeletedMessage eventData)
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
