using System;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;
using ProvidePostingType = Moedelo.Money.Enums.ProvidePostingType;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class MediationFeeMapper
    {
        public static MediationFeeResponseDto Map(MediationFeeResponse response)
        {
            return new MediationFeeResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                Bills = LinkedDocumentsMapper.MapBillsResponse(response.Bills),
                Documents = LinkedDocumentsMapper.MapDocumentsResponse(response.Documents),
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        private static NdsResponseDto MapNds(MediationFeeResponse operation)
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

        public static MediationFeeSaveRequest Map(MediationFeeSaveDto dto)
        {
            return new MediationFeeSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract.DocumentBaseId ?? 0L,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : default,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(dto.Bills),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(dto.Documents),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming)
            };
        }

        internal static MediationFeeSaveRequest ToSaveRequest(this ConfirmMediationFeeDto dto)
        {
            return new MediationFeeSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = 0L,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : default,
                ProvideInAccounting = true,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto },
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null
            };
        }
    }
}
