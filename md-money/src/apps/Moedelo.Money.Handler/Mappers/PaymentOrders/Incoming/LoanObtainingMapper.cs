using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class LoanObtainingMapper
    {
        public static LoanObtainingImportRequest Map(ImportLoanObtaining commandData)
        {
            return new LoanObtainingImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static LoanObtainingImportRequest Map(ImportWithMissingContractLoanObtaining commandData)
        {
            return new LoanObtainingImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Sum = commandData.Sum,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingContract,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static LoanObtainingImportRequest Map(ImportWithMissingContractorLoanObtaining commandData)
        {
            return new LoanObtainingImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static LoanObtainingImportRequest Map(ImportDuplicateLoanObtaining commandData)
        {
            return new LoanObtainingImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}
