using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.PaymentOrders.Import;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    internal static class OtherOutgoingMapper
    {
        internal static OtherOutgoingSaveRequest MapToSaveRequest(OtherOutgoingImportRequest request)
        {
            return new OtherOutgoingSaveRequest
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
                IsPaid = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static OtherOutgoingSaveRequest MapToSaveRequest(OtherOutgoingResponse response)
        {
            return new OtherOutgoingSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = response.Contractor,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                IncludeNds = response.IncludeNds,
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                TaxPostings = new TaxPostingsData
                {
                    ProvidePostingType = ProvidePostingType.ByHand
                },
                AccPosting = new OtherOutgoingCustomAccPosting()
            };
        }

        internal static OtherOutgoingDto MapToDto(OtherOutgoingSaveRequest request)
        {
            return new OtherOutgoingDto
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
                IsPaid = request.IsPaid,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId
            };
        }

        internal static AccPosting[] MapToPostings(OtherOutgoingCustomAccPosting posting)
        {
            return new[]
            {
                new AccPosting
                {
                    Date = posting.Date,
                    Sum = posting.Sum,
                    DebitCode = posting.DebitCode,
                    DebitSubconto = posting.DebitSubconto,
                    CreditCode = posting.CreditCode,
                    CreditSubconto = new[] { new Subconto { Id = posting.CreditSubconto } },
                    Description = posting.Description
                }
            };
        }

        internal static OtherOutgoingResponse MapToResponse(OtherOutgoingDto dto)
        {
            return new OtherOutgoingResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorRequisites(dto.Contractor),
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static OtherOutgoingCreated MapToCreatedMessage(OtherOutgoingSaveRequest request)
        {
            return new OtherOutgoingCreated
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
                IsPaid = request.IsPaid,
                Nds = MapNdsToMessage(request),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static OtherOutgoingUpdated MapToUpdatedMessage(OtherOutgoingSaveRequest request)
        {
            return new OtherOutgoingUpdated
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
                IsPaid = request.IsPaid,
                Nds = MapNdsToMessage(request),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static OtherOutgoingProvideRequired MapToProvideRequired(OtherOutgoingResponse response)
        {
            return new OtherOutgoingProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorWithRequisitesToKafka(response.Contractor),
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                Nds = response.IncludeNds
                    ? new Nds
                    {
                        NdsSum = response.NdsSum,
                        NdsType = response.NdsType
                    }
                    : null,
            };
        }

        internal static OtherOutgoingDeleted MapToDeleted(OtherOutgoingResponse response, long? newDocumentBaseId)
        {
            return new OtherOutgoingDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Contractor?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static Nds MapNdsToMessage(OtherOutgoingSaveRequest request)
        {
            return request.IncludeNds
                ? new Nds
                {
                    NdsSum = request.NdsSum,
                    NdsType = request.NdsType
                }
                : null;
        }
    }
}
