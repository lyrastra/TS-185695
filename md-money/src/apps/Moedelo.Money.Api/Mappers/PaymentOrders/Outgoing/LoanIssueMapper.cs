using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Commands;
using Moedelo.Money.Resources;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class LoanIssueMapper
    {
        public static LoanIssueResponseDto Map(LoanIssueResponse response)
        {
            return new LoanIssueResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                IsLongTermLoan = response.IsLongTermLoan,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static LoanIssueSaveRequest Map(LoanIssueSaveDto dto)
        {
            return new LoanIssueSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId ?? 0L,
                Sum = dto.Sum,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.LoanIssue
                    : dto.Description,
                IsLongTermLoan = dto.IsLongTermLoan,
                ProvideInAccounting = dto.IsPaid ? dto.ProvideInAccounting ?? true : false,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

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
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
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
                OperationState = OperationState.Duplicate,
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
                OperationState = OperationState.MissingContract,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
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
                OperationState = OperationState.MissingKontragent,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(LoanIssueResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Kontragent.Inn,
                PayeeSettlementAccount = response.Kontragent.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        internal static LoanIssueSaveRequest ToSaveRequest(this ConfirmLoanIssueDto dto)
        {
            return new LoanIssueSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                Sum = dto.Sum,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                TaxPostings = new TaxPostingsData(),
                IsPaid = true,
                ProvideInAccounting = true,
                IsLongTermLoan = false,
                ContractBaseId = 0,
            };
        } 
    }
}
