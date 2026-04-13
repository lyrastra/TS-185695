using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class TransferToAccountMapper
    {
        internal static TransferToAccountStateDefinition.State MapToState(
            this TransferToAccountCreated eventData,
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
                ToSettlementAccountId = eventData.ToSettlementAccountId,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToAccountStateDefinition.State MapToState(
            this TransferToAccountUpdated eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto toSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                ToSettlementAccountId = eventData.ToSettlementAccountId,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToAccountStateDefinition.State MapToState(
            this TransferToAccountDeleted eventData)
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
