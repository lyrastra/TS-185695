using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class WithdrawalOfProfitMapper
    {
        internal static WithdrawalOfProfitStateDefinition.State MapToState(
            this WithdrawalOfProfitCreated eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid
            };
        }

        internal static WithdrawalOfProfitStateDefinition.State MapToState(
            this WithdrawalOfProfitUpdated eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid
            };
        }

        internal static WithdrawalOfProfitStateDefinition.State MapToState(
            this WithdrawalOfProfitDeleted eventData)
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
