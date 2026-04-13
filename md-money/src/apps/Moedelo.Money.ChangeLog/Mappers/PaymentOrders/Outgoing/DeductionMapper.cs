using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Events;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class DeductionMapper
    {
        internal static DeductionStateDefinition.State MapToState(this DeductionCreated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            WorkerDto deductionWorker)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                DeductionWorkerName = deductionWorker?.FullName,
                DeductionWorkerId = eventData.DeductionWorkerId,
                PaymentPriority = (int) eventData.PaymentPriority,
                Kbk = eventData.Kbk,
                Oktmo = eventData.Oktmo,
                Uin = eventData.Uin,
                DeductionWorkerDocumentNumber = eventData.DeductionWorkerDocumentNumber,
                IsBudgetaryDebt = eventData.IsBudgetaryDebt,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static DeductionStateDefinition.State MapToState(this DeductionUpdated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            WorkerDto deductionWorker)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                DeductionWorkerName = deductionWorker?.FullName,
                DeductionWorkerId = eventData.DeductionWorkerId,
                PaymentPriority = (int) eventData.PaymentPriority,
                Kbk = eventData.Kbk,
                Oktmo = eventData.Oktmo,
                Uin = eventData.Uin,
                DeductionWorkerDocumentNumber = eventData.DeductionWorkerDocumentNumber,
                IsBudgetaryDebt = eventData.IsBudgetaryDebt,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static DeductionStateDefinition.State MapToState(
            this DeductionDeleted eventData)
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
