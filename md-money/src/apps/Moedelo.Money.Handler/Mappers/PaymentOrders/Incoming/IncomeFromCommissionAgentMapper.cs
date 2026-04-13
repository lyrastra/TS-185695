using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class IncomeFromCommissionAgentMapper
    {
        public static IncomeFromCommissionAgentImportRequest Map(ImportIncomeFromCommissionAgent commandData)
        {
            return new IncomeFromCommissionAgentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static IncomeFromCommissionAgentImportRequest Map(ImportDuplicateIncomeFromCommissionAgent commandData)
        {
            return new IncomeFromCommissionAgentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Description = commandData.Description,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static IncomeFromCommissionAgentImportRequest Map(ImportWithMissingContractIncomeFromCommissionAgent commandData)
        {
            return new IncomeFromCommissionAgentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Sum = commandData.Sum,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingContract,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static IncomeFromCommissionAgentImportRequest Map(ImportWithMissingContractorIncomeFromCommissionAgent commandData)
        {
            return new IncomeFromCommissionAgentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingCommissionAgent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}
