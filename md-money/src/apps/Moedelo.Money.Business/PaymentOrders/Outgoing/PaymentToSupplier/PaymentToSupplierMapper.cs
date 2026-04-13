using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    internal static class PaymentToSupplierMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(PaymentToSupplierSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static PaymentToSupplierDto MapToDto(PaymentToSupplierSaveRequest request)
        {
            return new PaymentToSupplierDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Kontragent)
                    : null,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                Description = request.Description,
                IsMainContractor = request.IsMainContractor,
                ProvideInAccounting = request.ProvideInAccounting,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                IsIgnoreNumber = request.IsIgnoreNumber,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentToSupplierResponse MapToResponse(PaymentToSupplierDto dto)
        {
            return new PaymentToSupplierResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                IsMainContractor = dto.IsMainContractor,
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                IsPaid = dto.IsPaid,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static PaymentToSupplierCreated MapToCreatedMessage(PaymentToSupplierSaveRequest request)
        {
            return new PaymentToSupplierCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                IsMainContractor = request.IsMainContractor,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                DocumentLinks = GetDocumentLinks(request.DocumentLinks),
                ReserveSum = request.ReserveSum,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static PaymentToSupplierUpdated MapToUpdatedMessage(PaymentToSupplierSaveRequest request)
        {
            return new PaymentToSupplierUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                IsMainContractor = request.IsMainContractor,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                DocumentLinks = GetDocumentLinks(request.DocumentLinks),
                InvoiceLinks = GetInvoiceLinks(request.InvoiceLinks),
                ReserveSum = request.ReserveSum,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentToSupplierProvideRequired MapToProvideRequired(PaymentToSupplierResponse response)
        {
            return new PaymentToSupplierProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
                IsMainContractor = response.IsMainContractor,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                Nds = response.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = response.NdsSum,
                        NdsType = response.NdsType
                    }
                    : null,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                DocumentLinks = GetDocumentLinks(response),
                InvoiceLinks = GetInvoiceLinks(response),
                ReserveSum = response.ReserveSum.GetOrThrow(),
                IsPaid = response.IsPaid
            };
        }

        internal static PaymentToSupplierDeleted MapToDeleted(PaymentToSupplierResponse response, long? newDocumentBaseId)
        {
            return new PaymentToSupplierDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static Kafka.Abstractions.Models.DocumentLink[] GetDocumentLinks(IReadOnlyCollection<DocumentLinkSaveRequest> documentLinks)
        {
            return documentLinks?.Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
            {
                DocumentBaseId = documentLink.DocumentBaseId,
                LinkSum = documentLink.LinkSum
            }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        private static Kafka.Abstractions.Models.InvoiceLink[] GetInvoiceLinks(IReadOnlyCollection<InvoiceLinkSaveRequest> invoiceLinks)
        {
            return invoiceLinks?.Select(documentLink => new Kafka.Abstractions.Models.InvoiceLink
            {
                DocumentBaseId = documentLink.DocumentBaseId,
            }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.InvoiceLink>();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] GetDocumentLinks(PaymentToSupplierResponse payment)
        {
            return payment.Documents
                .GetOrThrow()
                ?.Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                    LinkSum = documentLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        private static Kafka.Abstractions.Models.InvoiceLink[] GetInvoiceLinks(PaymentToSupplierResponse payment)
        {
            return payment.Invoices
                .GetOrThrow()
                ?.Select(documentLink => new Kafka.Abstractions.Models.InvoiceLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.InvoiceLink>();
        }

        internal static PaymentToSupplierSaveRequest MapToSaveRequest(PaymentToSupplierResponse response)
        {
            return new PaymentToSupplierSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Kontragent = response.Kontragent,
                IsMainContractor = response.IsMainContractor,
                ContractBaseId = response.Contract?.Data?.DocumentBaseId,
                Description = response.Description,
                IncludeNds = response.IncludeNds,
                IsPaid = response.IsPaid,
                NdsSum = response.NdsSum,
                NdsType = response.NdsType,
                Number = response.Number,
                ProvideInAccounting = response.ProvideInAccounting,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                DocumentLinks = response.Documents?.Data?.Select(MapDocumentLink).ToArray()
                                ?? Array.Empty<DocumentLinkSaveRequest>(),
                InvoiceLinks = response.Invoices?.Data?.Select(MapInvoiceLink).ToArray()
                               ?? Array.Empty<InvoiceLinkSaveRequest>(),
                ReserveSum = response.ReserveSum?.Data,
                TaxPostings = new TaxPostingsData
                {
                    ProvidePostingType = response.TaxPostingsInManualMode
                        ? ProvidePostingType.ByHand
                        : ProvidePostingType.Auto
                },
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static PaymentToSupplierSaveRequest MapToSaveRequest(PaymentToSupplierImportRequest request)
        {
            return new PaymentToSupplierSaveRequest
            {
                Date = request.Date,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                Description = request.Description,
                IncludeNds = request.IncludeNds,
                NdsSum = request.NdsSum,
                NdsType = request.NdsType,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                DocumentLinks = request.DocumentLinks,
                IsMainContractor = true,
                TaxPostings = new TaxPostingsData(),
                ProvideInAccounting = true,
                IsPaid = true,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsIgnoreNumber = request.IsIgnoreNumber,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        private static DocumentLinkSaveRequest MapDocumentLink(DocumentLink link)
        {
            return new DocumentLinkSaveRequest
            {
                DocumentBaseId = link.DocumentBaseId,
                LinkSum = link.LinkSum
            };
        }

        private static InvoiceLinkSaveRequest MapInvoiceLink(InvoiceLink link)
        {
            return new InvoiceLinkSaveRequest
            {
                DocumentBaseId = link.DocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            PaymentToSupplierSaveRequest request)
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
