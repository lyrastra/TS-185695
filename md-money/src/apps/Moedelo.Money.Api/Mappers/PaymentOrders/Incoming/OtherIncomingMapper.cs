using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.AccPosting;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.Other;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.TaxPostings;
using ProvidePostingType = Moedelo.Money.Enums.ProvidePostingType;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class OtherIncomingMapper
    {
        public static OtherIncomingResponseDto Map(OtherIncomingResponse response)
        {
            return new OtherIncomingResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Contractor),
                ContractorType = response.Contractor?.Type ?? ContractorType.Ip,
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                Bills = LinkedDocumentsMapper.MapBillsResponse(response.Bills),
                IsReadOnly = response.IsReadOnly,
                Nds = MapNds(response),
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                IsFromImport = response.IsFromImport,
                NoAutoDeleteOperation = response.OperationState == OperationState.NoAutoDelete,
                IsTargetIncome = response.IsTargetIncome
            };
        }

        public static OtherIncomingSaveRequest Map(OtherIncomingSaveDto dto)
        {
            return new OtherIncomingSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor, dto.ContractorType),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(dto.Bills),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                AccPosting = dto.ProvideInAccounting ?? false
                    ? MapCustomAccPosting(dto.Date, dto.AccountingPosting)
                    : null,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                OperationState = dto.NoAutoDeleteOperation
                    ? OperationState.NoAutoDelete
                    : OperationState.Default,
                IsTargetIncome = dto.IsTargetIncome ?? false
            };
        }

        private static OtherIncomingCustomAccPosting MapCustomAccPosting(DateTime date, IncomingCustomAccPostingDto dto)
        {
            return new OtherIncomingCustomAccPosting
            {
                Date = date.Date,
                Sum = dto.Sum,
                DebitSubconto = dto.DebitSubconto,
                CreditCode = (int)dto.CreditCode,
                CreditSubconto = dto.CreditSubconto == null ? Array.Empty<Subconto>() : Map(dto.CreditSubconto),
                Description = dto.Description
            };
        }

        private static Subconto[] Map(IReadOnlyCollection<SubcontoDto> creditSubconto)
        {
            return creditSubconto.Select(x => new Subconto { Id = x.Id, Name = x.Name }).ToArray();
        }

        private static NdsResponseDto MapNds(OtherIncomingResponse operation)
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

        internal static OtherIncomingSaveRequest ToSaveRequest(this ConfirmOtherIncomingDto dto)
        {
            return new OtherIncomingSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor),
                ContractBaseId = null,
                Description = dto.Description,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.ByHand },
                ProvideInAccounting = true,
                AccPosting = null,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false ? dto.Nds?.Type : null,
                NdsSum = dto.Nds?.IncludeNds ?? false ? dto.Nds?.Sum : null,
                TaxationSystemType = dto.TaxationSystemType,
                IsTargetIncome = dto.IsTargetIncome,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                IsOutsourceImportRuleApplied = dto.IsOutsourceImportRuleApplied,
            };
        }
    }
}
