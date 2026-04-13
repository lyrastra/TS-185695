using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class WithdrawalFromAccountMapper
    {
        internal static WithdrawalFromAccountStateDefinition.State MapToState(
            this WithdrawalFromAccountCreated eventData,
            SettlementAccountDto settlementAccount,
            FirmCashOrderDto cashOrder)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                CashOrderBaseId = eventData.CashOrderBaseId,
                CashOrderName = CashOrderMapper.MapToName(cashOrder),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static WithdrawalFromAccountStateDefinition.State MapToState(
            this WithdrawalFromAccountUpdated eventData,
            SettlementAccountDto settlementAccount,
            FirmCashOrderDto cashOrder)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                CashOrderBaseId = eventData.CashOrderBaseId,
                CashOrderName = CashOrderMapper.MapToName(cashOrder),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static WithdrawalFromAccountStateDefinition.State MapToState(
            this WithdrawalFromAccountDeleted eventData)
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
