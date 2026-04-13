using System;
using System.Linq;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    internal static class PaymentFromCustomerMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(PaymentFromCustomerSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static PaymentFromCustomerDto MapToDto(PaymentFromCustomerSaveRequest request)
        {
            return new PaymentFromCustomerDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Kontragent)
                    : null,
                IsMediation = request.IsMediation,
                MediationCommission = request.MediationCommissionSum,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                MediationNdsType = request.MediationNdsType,
                MediationNdsSum = request.MediationNdsSum,
                Description = request.Description,
                IsMainContractor = request.IsMainContractor,
                ProvideInAccounting = request.ProvideInAccounting,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentFromCustomerSaveRequest MapToSaveRequest(PaymentFromCustomerResponse response)
        {
            return new PaymentFromCustomerSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                IsMediation = response.IsMediation,
                MediationCommissionSum = response.MediationCommissionSum,
                IncludeNds = response.IncludeNds,
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                MediationNdsType = response.MediationNdsType,
                MediationNdsSum = response.MediationNdsSum,
                Description = response.Description,
                IsMainContractor = response.IsMainContractor,
                ProvideInAccounting = response.ProvideInAccounting,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                BillLinks = response.Bills.GetOrThrow()?.Select(x => new BillLinkSaveRequest { BillBaseId = x.DocumentBaseId, LinkSum = x.LinkSum }).ToArray() ?? Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = response.Documents.GetOrThrow()?.Select(x => new DocumentLinkSaveRequest { DocumentBaseId = x.DocumentBaseId, LinkSum = x.LinkSum }).ToArray() ?? Array.Empty<DocumentLinkSaveRequest>(),
                ReserveSum = response.ReserveSum.GetOrThrow(),
                InvoiceLinks = response.Invoices.GetOrThrow()?.Select(x => new InvoiceLinkSaveRequest { DocumentBaseId = x.DocumentBaseId }).ToArray() ?? Array.Empty<InvoiceLinkSaveRequest>(),
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static PaymentFromCustomerResponse MapToResponse(PaymentFromCustomerDto dto)
        {
            return new PaymentFromCustomerResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IsMediation = dto.IsMediation,
                MediationCommissionSum = dto.MediationCommission,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                MediationNdsType = dto.MediationNdsType,
                MediationNdsSum = dto.MediationNdsSum,
                IsMainContractor = dto.IsMainContractor,
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static PaymentFromCustomerCreated MapToCreatedMessage(PaymentFromCustomerSaveRequest request)
        {
            return new PaymentFromCustomerCreated
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
                MediationNds = request.MediationNdsType != null
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.MediationNdsSum,
                        NdsType = request.MediationNdsType
                    }
                    : null,
                IsMediation = request.IsMediation,
                MediationCommissionSum = request.MediationCommissionSum,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                BillLinks = GetBillLinks(request),
                DocumentLinks = GetDocumentLinks(request),
                ReserveSum = request.ReserveSum,
                OperationState = request.OperationState,
                TaxationSystemType = request.TaxationSystemType.Value,
                PatentId = request.PatentId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static PaymentFromCustomerUpdated MapToUpdatedMessage(PaymentFromCustomerSaveRequest request)
        {
            return new PaymentFromCustomerUpdated
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
                MediationNds = request.MediationNdsType != null
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.MediationNdsSum,
                        NdsType = request.MediationNdsType
                    }
                    : null,
                IsMediation = request.IsMediation,
                MediationCommissionSum = request.MediationCommissionSum,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                BillLinks = GetBillLinks(request),
                DocumentLinks = GetDocumentLinks(request),
                InvoiceLinks = GetInvoiceLinks(request),
                ReserveSum = request.ReserveSum,
                TaxationSystemType = request.TaxationSystemType.Value,
                PatentId = request.PatentId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        private static Kafka.Abstractions.Models.BillLink[] GetBillLinks(PaymentFromCustomerSaveRequest payment)
        {
            return payment.BillLinks
                .Select(billLink => new Kafka.Abstractions.Models.BillLink
                {
                    BillBaseId = billLink.BillBaseId,
                    LinkSum = billLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.BillLink>();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] GetDocumentLinks(PaymentFromCustomerSaveRequest payment)
        {
            return payment.DocumentLinks
                .Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                    LinkSum = documentLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        private static Kafka.Abstractions.Models.InvoiceLink[] GetInvoiceLinks(PaymentFromCustomerSaveRequest payment)
        {
            return payment.InvoiceLinks
                ?.Select(documentLink => new Kafka.Abstractions.Models.InvoiceLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.InvoiceLink>();
        }

        internal static PaymentFromCustomerSaveRequest MapToSaveRequest(PaymentFromCustomerImportRequest request)
        {
            return new PaymentFromCustomerSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                IsMediation = request.IsMediation,
                MediationCommissionSum = request.MediationCommissionSum,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                MediationNdsType = request.MediationNdsType,
                MediationNdsSum = request.MediationNdsSum,
                Description = request.Description,
                IsMainContractor = true,
                ProvideInAccounting = true,
                ContractBaseId = request.ContractBaseId,
                BillLinks = request.BillLinks,
                DocumentLinks = request.DocumentLinks,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                SourceFileId = request.SourceFileId,
                TaxPostings = new TaxPostingsData(),
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        public static PaymentFromCustomerProvideRequired MapToProvideRequiredEvent(PaymentFromCustomerResponse response)
        {
            return new PaymentFromCustomerProvideRequired
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
                MediationNds = response.MediationNdsSum != null
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = response.MediationNdsSum,
                        NdsType = response.MediationNdsType
                    }
                    : null,
                IsMediation = response.IsMediation,
                MediationCommissionSum = response.MediationCommissionSum,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                BillLinks = GetBillLinks(response),
                DocumentLinks = GetDocumentLinks(response),
                InvoiceLinks = GetInvoiceLinks(response),
                ReserveSum = response.ReserveSum.GetOrThrow(),
                TaxationSystemType = response.TaxationSystemType.Value,
                PatentId = response.PatentId,
            };
        }

        public static PaymentFromCustomerDeleted MapToDeleted(PaymentFromCustomerResponse response, long? newDocumentBaseId)
        {
            return new PaymentFromCustomerDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static Kafka.Abstractions.Models.BillLink[] GetBillLinks(PaymentFromCustomerResponse payment)
        {
            return payment.Bills
                .GetOrThrow()
                ?.Select(billLink => new Kafka.Abstractions.Models.BillLink
                {
                    BillBaseId = billLink.DocumentBaseId,
                    LinkSum = billLink.LinkSum
                })
                .ToArray();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] GetDocumentLinks(PaymentFromCustomerResponse payment)
        {
            return payment.Documents
                .GetOrThrow()
                ?.Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                    LinkSum = documentLink.LinkSum
                }).ToArray();
        }

        private static Kafka.Abstractions.Models.InvoiceLink[] GetInvoiceLinks(PaymentFromCustomerResponse payment)
        {
            return payment.Invoices
                .GetOrThrow()
                ?.Select(documentLink => new Kafka.Abstractions.Models.InvoiceLink
                {
                    DocumentBaseId = documentLink.DocumentBaseId,
                }).ToArray();
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            PaymentFromCustomerSaveRequest request)
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
    }
}
