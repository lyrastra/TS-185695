using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class RefundFromAccountablePersonMapper
    {
        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonCreated eventData,
            SettlementAccountDto settlementAccount,
            AdvanceStatementDto advanceStatement)
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
                AdvanceStatementBaseId = eventData.AdvanceStatementBaseId,
                AdvanceStatementName = advanceStatement.MapToName(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonUpdated eventData,
            SettlementAccountDto settlementAccount,
            AdvanceStatementDto advanceStatement)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                AdvanceStatementBaseId = eventData.AdvanceStatementBaseId,
                AdvanceStatementName = advanceStatement.MapToName(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonProvideRequired eventData,
            SettlementAccountDto settlementAccount,
            AdvanceStatementDto advanceStatement)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                AdvanceStatementBaseId = eventData.AdvanceStatementBaseId,
                AdvanceStatementName = advanceStatement.MapToName(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonDeleted eventData)
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
