using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyTransferToAccountMapper
    {
        internal static CurrencyTransferToAccountStateDefinition.State MapToState(
            this CurrencyTransferToAccountCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto toSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                ToSettlementAccountId = eventData.ToSettlementAccountId ?? 0,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount?.Currency.ToString()),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static CurrencyTransferToAccountStateDefinition.State MapToState(
            this CurrencyTransferToAccountUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto toSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                ToSettlementAccountId = eventData.ToSettlementAccountId ?? 0,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount?.Currency.ToString()),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static CurrencyTransferToAccountStateDefinition.State MapToState(
            this CurrencyTransferToAccountDeletedMessage eventData)
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
