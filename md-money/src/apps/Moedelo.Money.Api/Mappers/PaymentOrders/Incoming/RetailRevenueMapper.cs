using System;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class RetailRevenueMapper
    {
        public static RetailRevenueResponseDto Map(RetailRevenueResponse response)
        {
            return new RetailRevenueResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                Acquiring = response.AcquiringCommissionSum.HasValue
                    ? new AcquiringResponseDto
                    {
                        CommissionSum = response.AcquiringCommissionSum.Value,
                        CommissionDate = response.AcquiringCommissionDate ?? response.Date.Date,
                    }
                    : null,
                TaxationSystemType = response.TaxationSystemType,
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                PatentId = response.PatentId,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                SaleDate = response.SaleDate,
                IsMediation = response.IsMediation,
                IsFromImport = response.IsFromImport,
                Nds = MapNds(response)
            };
        }

        public static RetailRevenueSaveRequest Map(RetailRevenueSaveDto dto)
        {
            return new RetailRevenueSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                AcquiringCommissionSum = (dto.Acquiring?.IsAcquiring ?? false) && dto.Acquiring?.CommissionSum > 0
                    ? dto.Acquiring?.CommissionSum
                    : null,
                AcquiringCommissionDate = (dto.Acquiring?.IsAcquiring ?? false) && dto.Acquiring?.CommissionSum > 0
                    ? dto.Acquiring?.CommissionDate ?? dto.Date.Date
                    : null,
                TaxationSystemType = dto.TaxationSystemType,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                PatentId = dto.PatentId,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                IsMediation = dto.IsMediation,
                SaleDate = dto.SaleDate.HasValue && dto.SaleDate.Value != DateTime.MinValue
                    ? dto.SaleDate.Value.Date
                    : dto.Date.Date,
            };
        }
        
        public static RetailRevenueSaveRequest ToSaveRequest(this ConfirmRetailRevenueDto dto)
        {
            return new RetailRevenueSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                SaleDate = dto.SaleDate,
                AcquiringCommissionSum = dto.AcquiringCommissionSum,
                AcquiringCommissionDate = dto.AcquiringCommissionDate,
                IncludeNds = dto.AcquiringCommissionNds?.IncludeNds == true,
                NdsType = dto.AcquiringCommissionNds?.Type,
                NdsSum = dto.AcquiringCommissionNds?.Sum,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                IsMediation = dto.IsMediation,
                OperationState = OperationState.OutsourceApproved,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                OutsourceState = null
            };
        }
        
        private static NdsResponseDto MapNds(RetailRevenueResponse operation)
        {
            return new NdsResponseDto
            {
                IncludeNds = operation.IncludeNds,
                Type = operation.IncludeNds
                    ? operation.NdsType
                    : null,
                Sum = operation.IncludeNds
                    ? operation.NdsSum
                    : null
            };
        }
    }
}
