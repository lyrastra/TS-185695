using Moedelo.Money.Api.Models.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Commands;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class LoanReturnMapper
    {
        public static LoanReturnResponseDto Map(LoanReturnResponse response)
        {
            return new LoanReturnResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                LoanInterestSum = response.LoanInterestSum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                IsLongTermLoan = response.IsLongTermLoan,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static LoanReturnSaveRequest Map(LoanReturnSaveDto dto)
        {
            return new LoanReturnSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId ?? 0L,
                Sum = dto.Sum,
                LoanInterestSum = dto.LoanInterestSum,
                Description = dto.Description,
                IsLongTermLoan = dto.IsLongTermLoan,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming)
            };
        }

        public static LoanReturnImportRequest Map(ImportLoanReturn commandData)
        {
            return new LoanReturnImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                LoanInterestSum = commandData.LoanInterestSum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static LoanReturnImportRequest Map(ImportDuplicateLoanReturn commandData)
        {
            return new LoanReturnImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Sum = commandData.Sum,
                LoanInterestSum = commandData.LoanInterestSum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Duplicate,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static LoanReturnImportRequest Map(ImportWithMissingContractLoanReturn commandData)
        {
            return new LoanReturnImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Sum = commandData.Sum,
                LoanInterestSum = commandData.LoanInterestSum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.MissingContract,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static LoanReturnImportRequest Map(ImportWithMissingContractorLoanReturn commandData)
        {
            return new LoanReturnImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                LoanInterestSum = commandData.LoanInterestSum,
                Description = commandData.Description,
                IsLongTermLoan = commandData.IsLongTermLoan,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.MissingKontragent,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        internal static LoanReturnSaveRequest ToSaveRequest(this ConfirmLoanReturnDto dto)
        {
            return new LoanReturnSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = 0L,
                Description = dto.Description,
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                LoanInterestSum = dto.LoanInterestSum,
                IsLongTermLoan = false,
                TaxPostings = new TaxPostingsData(),
            };
        }
    }
}
