using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    internal static class AccrualOfInterestMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(AccrualOfInterestSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static AccrualOfInterestDto MapToDto(AccrualOfInterestSaveRequest operation)
        {
            return new AccrualOfInterestDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                SettlementAccountId = operation.SettlementAccountId,
                Description = operation.Description,
                TaxationSystemType = operation.TaxationSystemType.Value,
                ProvideInAccounting = operation.ProvideInAccounting,
                TaxPostingType = operation.TaxPostings.ProvidePostingType,
                DuplicateId = operation.DuplicateId,
                OperationState = operation.OperationState,
                OutsourceState = operation.OutsourceState,
            };
        }

        internal static AccrualOfInterestResponse MapToResponse(AccrualOfInterestDto dto)
        {
            return new AccrualOfInterestResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                TaxationSystemType = dto.TaxationSystemType,
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static AccrualOfInterestSaveRequest MapToSaveRequest(AccrualOfInterestResponse response)
        {
            return new AccrualOfInterestSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                TaxationSystemType = response.TaxationSystemType,
                ProvideInAccounting = response.ProvideInAccounting,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static AccrualOfInterestSaveRequest MapToSaveRequest(AccrualOfInterestImportRequest request)
        {
            return new AccrualOfInterestSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                TaxationSystemType = request.TaxationSystemType,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static AccrualOfInterestCreated MapToCreatedMessage(AccrualOfInterestSaveRequest request)
        {
            return new AccrualOfInterestCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                Sum = request.Sum,
                TaxationSystemType = request.TaxationSystemType.Value,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static AccrualOfInterestUpdated MapToUpdatedMessage(AccrualOfInterestSaveRequest request)
        {
            return new AccrualOfInterestUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                Sum = request.Sum,
                TaxationSystemType = request.TaxationSystemType.Value,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public static AccrualOfInterestProvideRequired MapToProvideRequired(AccrualOfInterestResponse response)
        {
            return new AccrualOfInterestProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                Sum = response.Sum,
                TaxationSystemType = response.TaxationSystemType.Value,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode
            };
        }

        public static AccrualOfInterestDeleted MapToDeleted(AccrualOfInterestResponse response, long? newDocumentBaseId)
        {
            return new AccrualOfInterestDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            AccrualOfInterestSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                Description = request.Description,
                Postings = request.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = request.DocumentBaseId,
                TaxationSystemType = request.TaxationSystemType
            };
        }
    }
}
