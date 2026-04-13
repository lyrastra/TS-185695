using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.WithdrawalOfProfit.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class WithdrawalOfProfitMapper
    {
        internal static WithdrawalOfProfitStateDefinition.State MapToState(
            this WithdrawalOfProfitCreated eventData,
            CashDto cash)
        {
            return new WithdrawalOfProfitStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment
            };
        }

        internal static WithdrawalOfProfitStateDefinition.State MapToState(
            this WithdrawalOfProfitUpdated eventData,
            CashDto cash)
        {
            return new WithdrawalOfProfitStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingProfitWithdrawing
                    ? eventData.OldOperationType.GetDescription()
                    : null
            };
        }

        internal static WithdrawalOfProfitStateDefinition.State MapToState(
            this WithdrawalOfProfitDeleted eventData)
        {
            return new WithdrawalOfProfitStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
