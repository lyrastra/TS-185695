using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;
using ProvidePostingType = Moedelo.Money.Enums.ProvidePostingType;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class OtherOutgoingMapper
    {
        public static OtherOutgoingResponseDto Map(OtherOutgoingResponse response)
        {
            return new OtherOutgoingResponseDto
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
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                Nds = MapNds(response),
                IsFromImport = response.IsFromImport,
                NoAutoDeleteOperation = response.OperationState == OperationState.NoAutoDelete
            };
        }
        
        private static NdsResponseDto MapNds(OtherOutgoingResponse operation)
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

        public static OtherOutgoingSaveRequest Map(OtherOutgoingSaveDto dto)
        {
            return new OtherOutgoingSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor, dto.ContractorType),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = dto.Description,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                AccPosting = MapCustomAccPosting(dto),
                IsPaid = dto.IsPaid,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                OperationState = dto.NoAutoDeleteOperation
                    ? OperationState.NoAutoDelete
                    : OperationState.Default,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        private static OtherOutgoingCustomAccPosting MapCustomAccPosting(OtherOutgoingSaveDto dto)
        {
            if (dto.ProvideInAccounting != true || !dto.IsPaid || dto.AccountingPosting == null)
            {
                return null;
            }

            var postingDto = dto.AccountingPosting;
            return new OtherOutgoingCustomAccPosting
            {
                Date = dto.Date.Date,
                Sum = postingDto.Sum,
                DebitSubconto = postingDto.DebitSubconto == null
                    ? Array.Empty<Subconto>()
                    : Map(postingDto.DebitSubconto),
                DebitCode = (int) postingDto.DebitCode,
                CreditSubconto = postingDto.CreditSubconto,
                Description = postingDto.Description
            };
        }
        
        private static Subconto[] Map(IReadOnlyCollection<SubcontoDto> debitSubconto)
        {
            return debitSubconto.Select(x => new Subconto { Id = x.Id, Name = x.Name }).ToArray();
        }

        public static OtherOutgoingSaveRequest ToSaveRequest(this ConfirmOtherOutgoingDto dto)
        {
            return new OtherOutgoingSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Description = dto.Description,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor),
                NdsSum = dto.Nds?.Sum,
                NdsType = dto.Nds?.Type,
                IncludeNds = dto.Nds?.IncludeNds == true,
                OperationState = OperationState.OutsourceApproved,
                ContractBaseId = null,
                ProvideInAccounting = true,
                AccPosting = null,
                TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.ByHand },
                IsPaid = true,
                OutsourceState = null,
                IsOutsourceImportRuleApplied = dto.IsOutsourceImportRuleApplied,
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(OtherOutgoingResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Contractor.Inn,
                PayeeSettlementAccount = response.Contractor.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }
    }
}
