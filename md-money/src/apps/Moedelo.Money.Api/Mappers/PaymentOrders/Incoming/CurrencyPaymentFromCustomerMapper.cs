using System;
using System.Linq;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class CurrencyPaymentFromCustomerMapper
    {
        public static CurrencyPaymentFromCustomerResponseDto Map(CurrencyPaymentFromCustomerResponse response)
        {
            return new CurrencyPaymentFromCustomerResponseDto
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
                IsReadOnly = response.IsReadOnly,
                Documents = LinkedDocumentsMapper.MapDocumentsResponse(response.Documents),
                TotalSum = response.TotalSum,
                IsFromImport = response.IsFromImport,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId
            };
        }

        private static NdsResponseDto MapNds(CurrencyPaymentFromCustomerResponse operation)
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

        public static CurrencyPaymentFromCustomerSaveRequest Map(CurrencyPaymentFromCustomerSaveDto dto)
        {
            var result = new CurrencyPaymentFromCustomerSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                TotalSum = dto.TotalSum,
                LinkedDocuments = dto.Documents?.Select(Map).ToArray(),
                PatentId = dto.PatentId,
                TaxationSystemType = dto.TaxationSystemType
            };
            result.Kontragent.IsCurrency = true;
            return result;
        }

        private static DocumentLinkSaveRequest Map(DocumentLinkSaveDto dto)
        {
            return new DocumentLinkSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                LinkSum = dto.Sum
            };
        }

        internal static CurrencyPaymentFromCustomerSaveRequest ToSaveRequest(this ConfirmCurrencyPaymentFromCustomerDto dto)
        {
            if (dto is null)
            {
                return null;
            }
            
            var result = new CurrencyPaymentFromCustomerSaveRequest
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
                NdsType = dto.Nds?.Type,
                NdsSum = dto.Nds?.Sum,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                ProvideInAccounting = true,
                ContractBaseId = null,
                OperationState = dto.Contractor?.Id > 0
                    ? OperationState.OutsourceApproved
                    : OperationState.MissingKontragent,
                OutsourceState = null,
                LinkedDocuments = Array.Empty<DocumentLinkSaveRequest>(),
            };
            result.Kontragent.IsCurrency = true;

            return result;
        }
    }
}