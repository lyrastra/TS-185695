using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class LoanIssueMapper
    {
        public static LoanIssueImportRequest Map(ImportLoanIssue commandData)
        {
            return new LoanIssueImportRequest
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

        public static LoanIssueImportRequest Map(ImportDuplicateLoanIssue commandData)
        {
            return new LoanIssueImportRequest
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

        public static LoanIssueImportRequest Map(ImportWithMissingContractLoanIssue commandData)
        {
            return new LoanIssueImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingContract,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static LoanIssueImportRequest Map(ImportWithMissingContractorLoanIssue commandData)
        {
            return new LoanIssueImportRequest
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
    }
}
