using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class RefundFromAccountablePersonMapper
    {
        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonCreated eventData,
            CashDto cash,
            AdvanceStatementDto advanceStatement)
        {
            return new RefundFromAccountablePersonStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                AdvanceStatementBaseId = eventData.AdvanceStatementBaseId,
                AdvanceStatementName = advanceStatement.MapToName(),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonUpdated eventData,
            CashDto cash,
            AdvanceStatementDto advanceStatement)
        {
            return new RefundFromAccountablePersonStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                AdvanceStatementBaseId = eventData.AdvanceStatementBaseId,
                AdvanceStatementName = advanceStatement.MapToName(),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingReturnFromAccountablePerson
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonProvided eventData,
            CashDto cash,
            AdvanceStatementDto advanceStatement)
        {
            return new RefundFromAccountablePersonStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                AdvanceStatementBaseId = eventData.AdvanceStatementBaseId,
                AdvanceStatementName = advanceStatement.MapToName(),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonStateDefinition.State MapToState(
            this RefundFromAccountablePersonDeleted eventData)
        {
            return new RefundFromAccountablePersonStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
