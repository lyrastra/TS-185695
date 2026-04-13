using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyTransferFromAccountMapper
    {
        internal static CurrencyTransferFromAccountStateDefinition.State MapToState(
            this CurrencyTransferFromAccountCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId ?? 0,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount?.Currency.ToString()),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static CurrencyTransferFromAccountStateDefinition.State MapToState(
            this CurrencyTransferFromAccountUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount?.Currency.ToString()),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static CurrencyTransferFromAccountStateDefinition.State MapToState(
            this CurrencyTransferFromAccountDeletedMessage eventData)
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
