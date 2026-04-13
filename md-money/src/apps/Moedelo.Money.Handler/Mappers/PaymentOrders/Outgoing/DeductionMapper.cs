using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class DeductionMapper
    {
        public static DeductionImportRequest Map(ImportDeduction commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                DeductionWorkerId = commandData.DeductionWorkerId,
                DeductionWorkerInn = commandData.DeductionWorkerInn,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                DeductionWorkerDocumentNumber = commandData.DeductionWorkerDocumentNumber,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
            };
        }

        public static DeductionImportRequest Map(ImportDeductionDuplicate commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                DeductionWorkerId = commandData.DeductionWorkerId,
                DeductionWorkerInn = commandData.DeductionWorkerInn,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                DeductionWorkerDocumentNumber = commandData.DeductionWorkerDocumentNumber,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
            };
        }

        public static DeductionImportRequest Map(ImportDeductionWithMissingContractor commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
            };
        }

        public static DeductionImportRequest Map(ImportDeductionWithMissingEmployee commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingWorker,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
            };
        }
    }
}
