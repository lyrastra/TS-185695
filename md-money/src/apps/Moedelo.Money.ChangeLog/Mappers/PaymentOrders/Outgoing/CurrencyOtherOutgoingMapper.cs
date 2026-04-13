using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyOther.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyOtherOutgoingMapper
    {
        internal static CurrencyOtherOutgoingStateDefinition.State MapToState(
            this CurrencyOtherOutgoingCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId
            };
        }

        internal static CurrencyOtherOutgoingStateDefinition.State MapToState(
            this CurrencyOtherOutgoingUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId
            };
        }

        internal static CurrencyOtherOutgoingStateDefinition.State MapToState(
            this CurrencyOtherOutgoingDeletedMessage eventData)
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
