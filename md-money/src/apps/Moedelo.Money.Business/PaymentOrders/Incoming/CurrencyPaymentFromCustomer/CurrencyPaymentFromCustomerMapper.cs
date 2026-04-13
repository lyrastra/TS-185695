using System;
using System.Linq;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using DocumentLink = Moedelo.Money.Kafka.Abstractions.Models.DocumentLink;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    internal sealed class CurrencyPaymentFromCustomerMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(CurrencyPaymentFromCustomerSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static CurrencyPaymentFromCustomerDto MapToDto(CurrencyPaymentFromCustomerSaveRequest operation)
        {
            return new CurrencyPaymentFromCustomerDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                SettlementAccountId = operation.SettlementAccountId,
                Kontragent = operation.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(operation.Kontragent)
                    : null,
                Description = operation.Description,
                IncludeNds = operation.IncludeNds,
                NdsType = operation.NdsType,
                NdsSum = operation.NdsSum,
                ProvideInAccounting = operation.ProvideInAccounting,
                TaxPostingType = operation.TaxPostings.ProvidePostingType,
                DuplicateId = operation.DuplicateId,
                OperationState = operation.OperationState,
                SourceFileId = operation.SourceFileId,
                TotalSum = operation.TotalSum,
                PatentId = operation.PatentId,
                TaxationSystemType = operation.TaxationSystemType,
                OutsourceState = operation.OutsourceState,
            };
        }

        internal static CurrencyPaymentFromCustomerResponse MapToResponse(CurrencyPaymentFromCustomerDto dto)
        {
            return new CurrencyPaymentFromCustomerResponse
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
                TotalSum = dto.TotalSum,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                PatentId = dto.PatentId,
                TaxationSystemType = dto.TaxationSystemType,
                OutsourceState = dto.OutsourceState,
                OperationState = dto.OperationState,
            };
        }

        internal static CurrencyPaymentFromCustomerCreated MapToCreatedMessage(CurrencyPaymentFromCustomerSaveRequest request)
        {
            return new CurrencyPaymentFromCustomerCreated
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
                TotalSum = request.TotalSum,
                OperationState = request.OperationState,
                LinkedDocuments = request.LinkedDocuments?.Select(Map).ToArray() ?? Array.Empty<DocumentLink>(),
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyPaymentFromCustomerUpdated MapToUpdatedMessage(CurrencyPaymentFromCustomerSaveRequest request)
        {
            return new CurrencyPaymentFromCustomerUpdated
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
                TotalSum = request.TotalSum,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                OutsourceState = request.OutsourceState,
                OperationState = request.OperationState,

                LinkedDocuments = request.LinkedDocuments?.Select(Map).ToArray() ?? Array.Empty<DocumentLink>()
            };
        }

        internal static CurrencyPaymentFromCustomerSaveRequest MapToSaveRequest(CurrencyPaymentFromCustomerResponse response)
        {
            return new CurrencyPaymentFromCustomerSaveRequest
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
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                ProvideInAccounting = response.ProvideInAccounting,
                TotalSum = response.TotalSum,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        public static CurrencyPaymentFromCustomerSaveRequest MapToSaveRequest(CurrencyPaymentFromCustomerImportRequest request)
        {
            return new CurrencyPaymentFromCustomerSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                TotalSum = request.TotalSum,
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        private static DocumentLink Map(DocumentLinkSaveRequest request)
        {
            return new DocumentLink
            {
                DocumentBaseId = request.DocumentBaseId,
                LinkSum = request.LinkSum
            };
        }

        public static CurrencyPaymentFromCustomerProvideRequired MapToProvideRequired(CurrencyPaymentFromCustomerResponse response)
        {
            return new CurrencyPaymentFromCustomerProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
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
                TotalSum = response.TotalSum,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                LinkedDocuments = response.Documents
                    .GetOrThrow()?
                    .Select(d =>
                        new DocumentLink
                        {
                            DocumentBaseId = d.DocumentBaseId,
                            LinkSum = d.LinkSum
                        })
                    .ToArray() ?? Array.Empty<DocumentLink>()
            };
        }

        public static CurrencyPaymentFromCustomerDeleted MapToDeleted(CurrencyPaymentFromCustomerResponse response, long? newDocumentBaseId)
        {
            return new CurrencyPaymentFromCustomerDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            CurrencyPaymentFromCustomerSaveRequest request)
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