using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    internal static class CurrencyBankFeeMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(CurrencyBankFeeSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static CurrencyBankFeeDto MapToDto(CurrencyBankFeeSaveRequest request)
        {
            return new CurrencyBankFeeDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                TotalSum = request.TotalSum,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static CurrencyBankFeeResponse MapToResponse(CurrencyBankFeeDto dto)
        {
            return new CurrencyBankFeeResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                TotalSum = dto.TotalSum,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static CurrencyBankFeeCreatedMessage MapToCreatedMessage(CurrencyBankFeeSaveRequest request)
        {
            return new CurrencyBankFeeCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                TotalSum = request.TotalSum,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyBankFeeUpdatedMessage MapToUpdatedMessage(CurrencyBankFeeSaveRequest request)
        {
            return new CurrencyBankFeeUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                TotalSum = request.TotalSum,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static CurrencyBankFeeDeletedMessage MapToDeletedMessage(CurrencyBankFeeResponse response, long? newDocumentBaseId)
        {
            return new CurrencyBankFeeDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CurrencyBankFeeSaveRequest MapToSaveRequest(CurrencyBankFeeResponse response)
        {
            return new CurrencyBankFeeSaveRequest
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                TotalSum = response.TotalSum,
                DocumentBaseId = response.DocumentBaseId,
                SettlementAccountId = response.SettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                ProvideInAccounting = response.ProvideInAccounting,
            };
        }

        internal static CurrencyBankFeeSaveRequest MapToSaveRequest(CurrencyBankFeeImportRequest request)
        {
            return new CurrencyBankFeeSaveRequest
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                TotalSum = request.TotalSum,
                SettlementAccountId = request.SettlementAccountId,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            CurrencyBankFeeSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                Description = request.Description,
                Postings = request.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = request.DocumentBaseId
            };
        }
    }
}