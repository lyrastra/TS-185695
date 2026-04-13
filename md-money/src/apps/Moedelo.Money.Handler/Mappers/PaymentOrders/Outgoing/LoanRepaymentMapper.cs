using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class LoanRepaymentMapper
    {
        public static LoanRepaymentImportRequest Map(ImportLoanRepayment commandData)
        {
            return new LoanRepaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                LoanInterestSum = commandData.LoanInterestSum,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static LoanRepaymentImportRequest Map(ImportDuplicateLoanRepayment commandData)
        {
            return new LoanRepaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                LoanInterestSum = commandData.LoanInterestSum,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
            };
        }

        public static LoanRepaymentImportRequest Map(ImportWithMissingContractLoanRepayment commandData)
        {
            return new LoanRepaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                LoanInterestSum = commandData.LoanInterestSum,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingContract,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static LoanRepaymentImportRequest Map(ImportWithMissingContractorLoanRepayment commandData)
        {
            return new LoanRepaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                LoanInterestSum = commandData.LoanInterestSum,
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
