using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    internal static class CurrencyPaymentToSupplierMapper
    {
        internal static CurrencyPaymentToSupplierDto MapToDto(CurrencyPaymentToSupplierSaveRequest request)
        {
            return new CurrencyPaymentToSupplierDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                TotalSum = request.TotalSum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Kontragent)
                    : null,
                Description = request.Description,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId
            };
        }

        internal static CurrencyPaymentToSupplierResponse MapToResponse(CurrencyPaymentToSupplierDto dto)
        {
            return new CurrencyPaymentToSupplierResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                TotalSum = dto.TotalSum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static CurrencyPaymentToSupplierSaveRequest MapToSaveRequest(CurrencyPaymentToSupplierResponse response)
        {
            return new CurrencyPaymentToSupplierSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                IncludeNds = response.IncludeNds,
                NdsSum = response.NdsSum,
                NdsType = response.NdsType,
                TotalSum = response.TotalSum,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                ProvideInAccounting = response.ProvideInAccounting
            };
        }

        internal static CurrencyPaymentToSupplierSaveRequest MapToSaveRequest(CurrencyPaymentToSupplierImportRequest request)
        {
            return new CurrencyPaymentToSupplierSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                NdsType = request.NdsType,
                TotalSum = request.TotalSum,
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
            CurrencyPaymentToSupplierSaveRequest request)
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

        internal static CurrencyPaymentToSupplierCreatedMessage MapToCreatedMessage(CurrencyPaymentToSupplierSaveRequest request)
        {
            return new CurrencyPaymentToSupplierCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                ContractBaseId = request.ContractBaseId,
                DocumentLinks = MapDocumentLinks(request.DocumentLinks),
                Sum = request.Sum,
                TotalSum = request.TotalSum,
                Description = request.Description,

                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,

                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyPaymentToSupplierUpdatedMessage MapToUpdatedMessage(CurrencyPaymentToSupplierSaveRequest request)
        {
            return new CurrencyPaymentToSupplierUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                ContractBaseId = request.ContractBaseId,
                DocumentLinks = MapDocumentLinks(request.DocumentLinks),
                Sum = request.Sum,
                TotalSum = request.TotalSum,
                Description = request.Description,

                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,

                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                ProvideInAccounting = request.ProvideInAccounting,
                OldDocumentLinks = MapDocumentLinks(request.OldDocumentLinks),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState
            };
        }

        private static Kafka.Abstractions.Models.DocumentLink[] MapDocumentLinks(IReadOnlyCollection<DocumentLinkSaveRequest> links)
        {
            if (links == null)
            {
                return Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
            }

            return links
                .Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                    LinkSum = documentLink.LinkSum
                }).ToArray();
        }
    }
}
