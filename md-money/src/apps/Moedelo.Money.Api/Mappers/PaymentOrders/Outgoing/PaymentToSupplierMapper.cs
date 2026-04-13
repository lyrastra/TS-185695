using System;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands;
using Moedelo.Money.Resources;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class PaymentToSupplierMapper
    {
        public static PaymentToSupplierResponseDto Map(PaymentToSupplierResponse response)
        {
            return new PaymentToSupplierResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                IsMainContractor = response.IsMainContractor,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                Documents = LinkedDocumentsMapper.MapDocumentsResponse(response.Documents),
                ReserveSum = LinkedDocumentsMapper.MapReserveResponse(response.ReserveSum),
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static PaymentToSupplierApplyIgnoreNumberRequest Map(ApplyIgnoreNumberPaymentToSupplier commandData)
        {
            return new PaymentToSupplierApplyIgnoreNumberRequest
            {
                DocumentBaseIds = commandData.DocumentBaseIds,
                ImportRuleId = commandData.ImportRuleId
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(PaymentToSupplierResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Kontragent.Inn,
                PayeeSettlementAccount = response.Kontragent.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        private static NdsResponseDto MapNds(PaymentToSupplierResponse operation)
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

        public static PaymentToSupplierSaveRequest Map(PaymentToSupplierSaveDto dto)
        {
            return new PaymentToSupplierSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.PaymentToSupplier
                    : dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                IsMainContractor = dto.IsMainContractor ?? true,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(dto.Documents),
                ReserveSum = dto.ReserveSum,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static PaymentToSupplierSaveRequest ToSaveRequest(this ConfirmPaymentToSupplierDto dto)
        {
            return new PaymentToSupplierSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Description = dto.Description,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                NdsSum = dto.Nds?.Sum,
                NdsType = dto.Nds?.Type,
                IncludeNds = dto.Nds?.IncludeNds == true,
                OperationState = OperationState.OutsourceApproved,
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                ContractBaseId = null,
                InvoiceLinks = Array.Empty<InvoiceLinkSaveRequest>(),
                ReserveSum = null,
                IsMainContractor = true,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                IsPaid = true,
                OutsourceState = null,
            };
        }
    }
}
