using System;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Money.Resources;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class CurrencyPaymentToSupplierMapper
    {
        public static CurrencyPaymentToSupplierResponseDto Map(CurrencyPaymentToSupplierResponse response)
        {
            return new CurrencyPaymentToSupplierResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                TotalSum = response.TotalSum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Documents = LinkedDocumentsMapper.MapDocumentsResponse(response.Documents),
                Description = response.Description,
                Nds = MapNds(response),
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        private static NdsResponseDto MapNds(CurrencyPaymentToSupplierResponse operation)
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

        public static CurrencyPaymentToSupplierSaveRequest Map(CurrencyPaymentToSupplierSaveDto dto)
        {
            var result = new CurrencyPaymentToSupplierSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                TotalSum = dto.TotalSum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
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
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(dto.Documents),
                IsSaveNumeration = dto.IsSaveNumeration
            };
            result.Kontragent.IsCurrency = true;
            return result;
        }

        public static CurrencyPaymentToSupplierSaveRequest ToSaveRequest(this ConfirmCurrencyPaymentToSupplierDto dto)
        {
            var result = new CurrencyPaymentToSupplierSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                TotalSum = 0, // рассчитывается на основании курса ЦБ
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds == true,
                NdsSum = dto.Nds?.Sum,
                NdsType = dto.Nds?.Type,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),
                ProvideInAccounting = true,
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                ContractBaseId = null,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                OldDocumentLinks = Array.Empty<DocumentLinkSaveRequest>()
            };
            result.Kontragent.IsCurrency = true;
            return result;
        }
    }
}
