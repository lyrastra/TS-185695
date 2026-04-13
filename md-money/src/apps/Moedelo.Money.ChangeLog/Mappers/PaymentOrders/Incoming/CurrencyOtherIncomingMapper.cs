using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyOther.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyOtherIncomingMapper
    {
        internal static CurrencyOtherIncomingStateDefinition.State MapToState(
            this CurrencyOtherIncomingCreatedMessage eventData,
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
                ProvideInAccounting = eventData.ProvideInAccounting,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId
            };
        }

        internal static CurrencyOtherIncomingStateDefinition.State MapToState(
            this CurrencyOtherIncomingUpdatedMessage eventData,
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
                ProvideInAccounting = eventData.ProvideInAccounting,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId
            };
        }

        internal static CurrencyOtherIncomingStateDefinition.State MapToState(
            this CurrencyOtherIncomingDeletedMessage eventData)
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
