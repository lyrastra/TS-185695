using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System;
using System.Linq;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Business.Kontragents;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    internal static class OtherIncomingMapper
    {
        internal static OtherIncomingDto MapToDto(OtherIncomingSaveRequest request)
        {
            return new OtherIncomingDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapContractorRequisitesToDto(request.Contractor)
                    : null,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                DuplicateId = request.DuplicateId,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                IsTargetIncome = request.IsTargetIncome
            };
        }

        internal static OtherIncomingSaveRequest MapToSaveRequest(OtherIncomingResponse response)
        {
            return new OtherIncomingSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = response.Contractor,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IncludeNds = response.IncludeNds,
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                DuplicateId = response.DuplicateId,
                BillLinks = response.Bills
                    .GetOrThrow()
                    ?.Select(x => new BillLinkSaveRequest { BillBaseId = x.DocumentBaseId, LinkSum = x.LinkSum })
                    .ToArray() ?? Array.Empty<BillLinkSaveRequest>(),
                IsTargetIncome = response.IsTargetIncome,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                
            };
        }

        internal static AccPosting[] MapToPostings(OtherIncomingCustomAccPosting posting)
        {
            return new[]
            {
                new AccPosting
                {
                    Date = posting.Date,
                    Sum = posting.Sum,
                    DebitCode = posting.DebitCode,
                    DebitSubconto = new[] { new Subconto { Id = posting.DebitSubconto } },
                    CreditCode = posting.CreditCode,
                    CreditSubconto = posting.CreditSubconto,
                    Description = posting.Description
                }
            };
        }

        internal static OtherIncomingResponse MapToResponse(OtherIncomingDto dto)
        {
            return new OtherIncomingResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorRequisites(dto.Contractor),
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                DuplicateId = dto.DuplicateId,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsTargetIncome = dto.IsTargetIncome
            };
        }

        internal static OtherIncomingCreatedMessage MapToCreatedMessage(OtherIncomingSaveRequest request)
        {
            return new OtherIncomingCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapContractorWithRequisitesToKafka(request.Contractor)
                    : null,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                BillLinks = GetBillLinks(request),
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                TaxationSystemType = request.TaxationSystemType,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsTargetIncome = request.IsTargetIncome,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static OtherIncomingUpdatedMessage MapToUpdatedMessage(OtherIncomingSaveRequest request)
        {
            return new OtherIncomingUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorWithRequisitesToKafka(request.Contractor),
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                BillLinks = GetBillLinks(request),
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                TaxationSystemType = request.TaxationSystemType,
                IsTargetIncome = request.IsTargetIncome,
                OutsourceState = request.OutsourceState,
                OperationState = request.OperationState
            };
        }

        internal static OtherIncomingDeletedMessage MapToDeletedMessage(OtherIncomingResponse response, long? newDocumentBaseId)
        {
            return new OtherIncomingDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Contractor?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static Kafka.Abstractions.Models.BillLink[] GetBillLinks(OtherIncomingSaveRequest payment)
        {
            return payment.BillLinks
                ?.Select(billLink => new Kafka.Abstractions.Models.BillLink
                {
                    BillBaseId = billLink.BillBaseId,
                    LinkSum = billLink.LinkSum
                }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.BillLink>();
        }

        internal static OtherIncomingSaveRequest MapToSaveRequest(ImportOtherIncomingRequest request)
        {
            return new OtherIncomingSaveRequest()
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.Contractor,
                ContractBaseId = request.ContractBaseId,
                ProvideInAccounting = true,
                TaxPostings = request.TaxPosting.MapToSaveRequest(),
                AccPosting = request.AccPosting,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                OperationState = request.OperationState,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }
    }
}
