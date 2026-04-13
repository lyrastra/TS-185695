using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class CurrencyOtherOutgoingMapper
    {
        public static CurrencyOtherOutgoingResponseDto Map(CurrencyOtherOutgoingResponse response)
        {
            return new CurrencyOtherOutgoingResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                TotalSum = response.TotalSum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Contactor),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                IsReadOnly = response.IsReadOnly,
                ContractorType = response.Contactor.Type,
                ProvideInAccounting = response.ProvideInAccounting,
            };
        }

        private static NdsResponseDto MapNds(CurrencyOtherOutgoingResponse operation)
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

        public static CurrencyOtherOutgoingSaveRequest Map(CurrencyOtherOutgoingSaveDto dto)
        {
            var result = new CurrencyOtherOutgoingSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                TotalSum = dto.TotalSum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor, dto.ContractorType),
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
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                AccPosting = MapCustomAccPosting(dto),
                IsSaveNumeration = dto.IsSaveNumeration
            };
            result.Contractor.IsCurrency = true;
            return result;
        }

        public static CurrencyOtherOutgoingSaveRequest ToSaveRequest(this ConfirmCurrencyOtherOutgoingDto dto)
        {
            var result = new CurrencyOtherOutgoingSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor),
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds == true,
                NdsSum = dto.Nds?.Sum,
                NdsType = dto.Nds?.Type,
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),
                ContractBaseId = null,
                TotalSum = default,
                AccPosting = null,
            };

            result.Contractor.IsCurrency = true;
            return result;
        }

        private static CurrencyOtherOutgoingCustomAccPosting MapCustomAccPosting(CurrencyOtherOutgoingSaveDto dto)
        {
            if (dto.ProvideInAccounting == false || dto.AccountingPosting == null)
            {
                return null;
            }

            var postingDto = dto.AccountingPosting;
            return new CurrencyOtherOutgoingCustomAccPosting
            {
                Date = dto.Date.Date,
                Sum = postingDto.Sum,
                DebitSubconto = Map(postingDto.DebitSubconto),
                DebitCode = (int)postingDto.DebitCode,
                CreditSubconto = postingDto.CreditSubconto,
                Description = postingDto.Description
            };
        }

        private static Subconto[] Map(IReadOnlyCollection<SubcontoDto> debitSubconto)
        {
            return debitSubconto != null
                ? debitSubconto.Select(x => new Subconto { Id = x.Id, Name = x.Name }).ToArray()
                : Array.Empty<Subconto>();
        }
    }
}