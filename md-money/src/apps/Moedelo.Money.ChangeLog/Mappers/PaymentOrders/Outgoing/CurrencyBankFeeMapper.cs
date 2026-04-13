using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyBankFeeMapper
    {
        internal static CurrencyBankFeeStateDefinition.State MapToState(
            this CurrencyBankFeeCreatedMessage eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount.Currency.ToString()),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static CurrencyBankFeeStateDefinition.State MapToState(
            this CurrencyBankFeeUpdatedMessage eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount.Currency.ToString()),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static CurrencyBankFeeStateDefinition.State MapToState(
            this CurrencyBankFeeDeletedMessage eventData)
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
