using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    internal static class MediationFeeMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(MediationFeeSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static MediationFeeDto MapToDto(MediationFeeSaveRequest operation)
        {
            return new MediationFeeDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                SettlementAccountId = operation.SettlementAccountId,
                Kontragent = operation.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(operation.Kontragent)
                    : null,
                IncludeNds = operation.IncludeNds,
                NdsType = operation.NdsType,
                NdsSum = operation.NdsSum,
                Description = operation.Description,
                ProvideInAccounting = operation.ProvideInAccounting,
                TaxPostingType = operation.TaxPostings.ProvidePostingType,
                DuplicateId = operation.DuplicateId,
                OperationState = operation.OperationState,
                SourceFileId = operation.SourceFileId,
                OutsourceState = operation.OutsourceState,
            };
        }

        internal static MediationFeeSaveRequest MapToSaveRequest(MediationFeeResponse response)
        {
            return new MediationFeeSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                ContractBaseId = response.Contract.GetOrThrow().DocumentBaseId,
                IncludeNds = response.IncludeNds,
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                BillLinks = response.Bills.GetOrThrow()?.Select(x => new BillLinkSaveRequest { BillBaseId = x.DocumentBaseId, LinkSum = x.LinkSum }).ToArray() ?? Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = response.Documents.GetOrThrow()?.Select(x => new DocumentLinkSaveRequest { DocumentBaseId = x.DocumentBaseId, LinkSum = x.LinkSum }).ToArray() ?? Array.Empty<DocumentLinkSaveRequest>(),
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static MediationFeeResponse MapToResponse(MediationFeeDto dto)
        {
            return new MediationFeeResponse
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
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static MediationFeeCreated MapToCreatedMessage(MediationFeeSaveRequest request)
        {
            return new MediationFeeCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
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
                DocumentLinks = GetDocumentLinks(request),
                BillLinks = GetBillLinks(request),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static MediationFeeUpdated MapToUpdatedMessage(MediationFeeSaveRequest request)
        {
            return new MediationFeeUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
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
                DocumentLinks = GetDocumentLinks(request),
                BillLinks = GetBillLinks(request),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static MediationFeeProvideRequired MapToProvideRequired(MediationFeeResponse response)
        {
            return new MediationFeeProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
                ContractBaseId = response.Contract.GetOrThrow().DocumentBaseId,
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
                DocumentLinks = GetDocumentLinks(response.Documents.GetOrThrow()),
                BillLinks = GetBillLinks(response.Bills.GetOrThrow()),
            };
        }

        internal static MediationFeeSaveRequest MapToSaveRequest(MediationFeeImportRequest request)
        {
            return new MediationFeeSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                Description = request.Description,
                ProvideInAccounting = true,
                ContractBaseId = request.ContractBaseId,
                BillLinks = request.BillLinks,
                DocumentLinks = request.DocumentLinks,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                TaxPostings = new TaxPostingsData(),
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static MediationFeeDeleted MapToDeleted(MediationFeeResponse response, long? newDocumentBaseId)
        {
            return new MediationFeeDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static Kafka.Abstractions.Models.BillLink[] GetBillLinks(MediationFeeSaveRequest payment)
        {
            return payment.BillLinks
                .Select(billLink => new Kafka.Abstractions.Models.BillLink
                {
                    BillBaseId = billLink.BillBaseId,
                    LinkSum = billLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.BillLink>();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] GetDocumentLinks(MediationFeeSaveRequest payment)
        {
            return payment.DocumentLinks
                .Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                    LinkSum = documentLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        private static Kafka.Abstractions.Models.BillLink[] GetBillLinks(IReadOnlyCollection<BillLink> bills)
        {
            return bills?
                .Select(billLink => new Kafka.Abstractions.Models.BillLink
                {
                    BillBaseId = billLink.DocumentBaseId,
                    LinkSum = billLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.BillLink>();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] GetDocumentLinks(IReadOnlyCollection<DocumentLink> documents)
        {
            return documents?
                .Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                    LinkSum = documentLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            MediationFeeSaveRequest request)
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
