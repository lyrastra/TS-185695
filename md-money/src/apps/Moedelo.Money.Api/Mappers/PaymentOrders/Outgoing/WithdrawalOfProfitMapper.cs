using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class WithdrawalOfProfitMapper
    {
        public static WithdrawalOfProfitResponseDto Map(WithdrawalOfProfitResponse response)
        {
            return new WithdrawalOfProfitResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Description = response.Description,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static WithdrawalOfProfitSaveRequest Map(WithdrawalOfProfitSaveDto dto)
        {
            return new WithdrawalOfProfitSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.WithdrawalOfProfit
                    : dto.Description,
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static WithdrawalOfProfitImportRequest Map(ImportWithdrawalOfProfit commandData)
        {
            return new WithdrawalOfProfitImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static WithdrawalOfProfitImportRequest Map(ImportDuplicateWithdrawalOfProfit commandData)
        {
            return new WithdrawalOfProfitImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Description = commandData.Description,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Duplicate,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static WithdrawalOfProfitImportRequest Map(ImportWithMissingContractorWithdrawalOfProfit commandData)
        {
            return new WithdrawalOfProfitImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.MissingKontragent,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static WithdrawalOfProfitApplyIgnoreNumberRequest Map(ApplyIgnoreNumberWithdrawalOfProfit commandData)
        {
            return new WithdrawalOfProfitApplyIgnoreNumberRequest
            {
                DocumentBaseIds = commandData.DocumentBaseIds,
                ImportRuleId = commandData.ImportRuleId
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(WithdrawalOfProfitResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Kontragent.Inn,
                PayeeSettlementAccount = response.Kontragent.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        internal static WithdrawalOfProfitSaveRequest ToSaveRequest(this ConfirmWithdrawalOfProfitDto dto)
        {
            return new WithdrawalOfProfitSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Description = dto.Description,
                Number = dto.Number,
                Date = dto.Date,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                IsPaid = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
