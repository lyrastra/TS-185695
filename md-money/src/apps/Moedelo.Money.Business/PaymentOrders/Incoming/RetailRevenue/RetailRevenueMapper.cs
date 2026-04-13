using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    internal static class RetailRevenueMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(RetailRevenueSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static RetailRevenueDto MapToDto(RetailRevenueSaveRequest request)
        {
            return new RetailRevenueDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                SettlementAccountId = request.SettlementAccountId,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                AcquiringCommissionSum = request.AcquiringCommissionSum,
                AcquiringCommissionDate = request.AcquiringCommissionDate,
                TaxationSystemType = request.TaxationSystemType,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                PatentId = request.PatentId,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                IsMediation = request.IsMediation,
                SaleDate = request.SaleDate,
                OutsourceState = request.OutsourceState,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                IncludeNds = request.IncludeNds
            };
        }

        internal static RetailRevenueSaveRequest MapToSaveRequest(RetailRevenueResponse response)
        {
            return new RetailRevenueSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                SettlementAccountId = response.SettlementAccountId,
                Number = response.Number,
                Sum = response.Sum,
                Description = response.Description,
                AcquiringCommissionSum = response.AcquiringCommissionSum,
                AcquiringCommissionDate = response.AcquiringCommissionDate,
                TaxationSystemType = response.TaxationSystemType,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                SaleDate = response.SaleDate,
                IsMediation = response.IsMediation,
                PatentId = response.PatentId,
                OutsourceState = response.OutsourceState,
                IncludeNds = response.IncludeNds,
                NdsSum = response.NdsSum,
                NdsType = response.NdsType
            };
        }

        internal static RetailRevenueSaveRequest MapToSaveRequest(RetailRevenueImportRequest request)
        {
            return new RetailRevenueSaveRequest
            {
                Date = request.Date,
                SettlementAccountId = request.SettlementAccountId,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                AcquiringCommissionSum = request.AcquiringCommissionSum,
                AcquiringCommissionDate = request.AcquiringCommissionDate,
                TaxationSystemType = request.TaxationSystemType,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                IsMediation = request.IsMediation,
                SaleDate = request.Date,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                NdsType = request.NdsType
            };
        }

        internal static RetailRevenueResponse MapToResponse(RetailRevenueDto dto)
        {
            return new RetailRevenueResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                AcquiringCommissionSum = dto.AcquiringCommissionSum,
                AcquiringCommissionDate = dto.AcquiringCommissionDate,
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxationSystemType = dto.TaxationSystemType,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                PatentId = dto.PatentId,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                IsMediation = dto.IsMediation,
                SaleDate = dto.SaleDate,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                IncludeNds = dto.IncludeNds
            };
        }

        internal static RetailRevenueCreated MapToCreatedMessage(RetailRevenueSaveRequest request)
        {
            return new RetailRevenueCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                AcquiringCommissionSum = request.AcquiringCommissionSum,
                AcquiringCommissionDate = request.AcquiringCommissionDate,
                TaxationSystemType = request.TaxationSystemType.Value,
                ProvideInAccounting = request.ProvideInAccounting,
                Nds = MapNds(request),
                IsMediation = request.IsMediation,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                PatentId = request.PatentId,
                OperationState = request.OperationState,
                SaleDate = request.SaleDate,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static RetailRevenueUpdated MapToUpdatedMessage(RetailRevenueSaveRequest request)
        {
            return new RetailRevenueUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                Nds = MapNds(request),
                AcquiringCommissionSum = request.AcquiringCommissionSum,
                AcquiringCommissionDate = request.AcquiringCommissionDate,
                TaxationSystemType = request.TaxationSystemType.Value,
                PatentId = request.PatentId,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsMediation = request.IsMediation,
                SaleDate = request.SaleDate,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static RetailRevenueProvideRequired MapToProvideRequired(RetailRevenueResponse response)
        {
            return new RetailRevenueProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                Nds = MapNds(response),
                AcquiringCommissionSum = response.AcquiringCommissionSum,
                AcquiringCommissionDate = response.AcquiringCommissionDate,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                TaxationSystemType = response.TaxationSystemType.Value,
                PatentId = response.PatentId,
                IsMediation = response.IsMediation,
                SaleDate = response.SaleDate
            };
        }

        internal static RetailRevenueDeleted MapToDeleted(RetailRevenueResponse response, long? newDocumentBaseId)
        {
            return new RetailRevenueDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            RetailRevenueSaveRequest request)
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
        
        private static Nds MapNds(RetailRevenueSaveRequest request)
        {
            return request.IncludeNds
                ? new Nds
                {
                    NdsSum = request.NdsSum,
                    NdsType = request.NdsType
                }
                : null;
        }

        private static Nds MapNds(RetailRevenueResponse response)
        {
            return response.IncludeNds
                ? new Nds
                {
                    NdsSum = response.NdsSum,
                    NdsType = response.NdsType
                }
                : null;
        }
    }
}
