using System;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class PaymentFromCustomerMapper
    {
        public static PaymentFromCustomerResponseDto Map(PaymentFromCustomerResponse response)
        {
            return new PaymentFromCustomerResponseDto
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                MediationNds = MapMediationNds(response),
                Mediation = new MediationResponseDto
                {
                    IsMediation = response.IsMediation,
                    CommissionSum = response.MediationCommissionSum,
                },
                IsMainContractor = response.IsMainContractor,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                Bills = LinkedDocumentsMapper.MapBillsResponse(response.Bills),
                Documents = LinkedDocumentsMapper.MapDocumentsResponse(response.Documents),
                ReserveSum = LinkedDocumentsMapper.MapReserveResponse(response.ReserveSum),
                IsReadOnly = response.IsReadOnly,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                IsFromImport = response.IsFromImport
            };
        }

        private static NdsResponseDto MapNds(PaymentFromCustomerResponse operation)
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

        private static NdsResponseDto MapMediationNds(PaymentFromCustomerResponse operation)
        {
            return new NdsResponseDto
            {
                IncludeNds = operation.MediationNdsType != null,
                Type = operation.MediationNdsType,
                Sum = operation.MediationNdsSum
            };
        }

        public static PaymentFromCustomerSaveRequest Map(PaymentFromCustomerSaveDto dto)
        {
            return new PaymentFromCustomerSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = dto.Description,
                IsMediation = dto.Mediation?.IsMediation ?? false,
                MediationCommissionSum = dto.Mediation?.IsMediation ?? false
                    ? dto.Mediation?.CommissionSum
                    : default,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                MediationNdsType = dto.MediationNds?.IncludeNds ?? false
                    ? dto.MediationNds.Type
                    : null,
                MediationNdsSum = dto.MediationNds?.IncludeNds ?? false
                    ? dto.MediationNds.Sum
                    : null,
                IsMainContractor = dto.IsMainContractor ?? true,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(dto.Bills),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(dto.Documents),
                ReserveSum = dto.ReserveSum,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId
            };
        }
        
        public static PaymentFromCustomerSaveRequest ToSaveRequest(this ConfirmPaymentFromCustomerDto dto)
        {
            return new PaymentFromCustomerSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                IsMediation = dto.IsMediation,
                MediationCommissionSum = dto.MediationCommissionSum,
                IncludeNds = dto.Nds?.IncludeNds == true,
                NdsType = dto.Nds?.Type,
                NdsSum = dto.Nds?.Sum,
                MediationNdsType = dto.MediationNds?.IncludeNds ?? false
                    ? dto.MediationNds.Type
                    : null,
                MediationNdsSum = dto.MediationNds?.IncludeNds ?? false
                    ? dto.MediationNds.Sum
                    : null,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                OperationState = OperationState.OutsourceApproved,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                ContractBaseId = null,
                InvoiceLinks = Array.Empty<InvoiceLinkSaveRequest>(),
                ReserveSum = null,
                IsMainContractor = true,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                OutsourceState = null,
            };
        }
    }
}
