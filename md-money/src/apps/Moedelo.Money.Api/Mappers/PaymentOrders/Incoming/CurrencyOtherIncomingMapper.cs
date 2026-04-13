using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class CurrencyOtherIncomingMapper
    {
        public static CurrencyOtherIncomingResponseDto Map(CurrencyOtherIncomingResponse response)
        {
            return new CurrencyOtherIncomingResponseDto
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
                ProvideInAccounting = response.ProvideInAccounting
            };
        }

        public static CurrencyOtherIncomingSaveRequest Map(CurrencyOtherIncomingSaveDto dto)
        {
            var result = new CurrencyOtherIncomingSaveRequest
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
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                AccPosting = MapCustomAccPosting(dto)
            };
            result.Contractor.IsCurrency = true;
            return result;
        }

        public static CurrencyOtherIncomingSaveRequest ToSaveRequest(this ConfirmCurrencyOtherIncomingDto dto)
        {
            var result = new CurrencyOtherIncomingSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                IncludeNds = dto.Nds?.IncludeNds == true,
                NdsType = dto.Nds?.Type,
                NdsSum = dto.Nds?.Sum,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor),
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                ContractBaseId = null,
                TotalSum = default,
                AccPosting = null,
            };
            result.Contractor.IsCurrency = true;
            return result;
        }

        private static NdsResponseDto MapNds(CurrencyOtherIncomingResponse operation)
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

        private static CurrencyOtherIncomingCustomAccPosting MapCustomAccPosting(CurrencyOtherIncomingSaveDto dto)
        {
            if (dto.ProvideInAccounting == false || dto.AccountingPosting == null)
            {
                return null;
            }

            return new CurrencyOtherIncomingCustomAccPosting
            {
                Date = dto.Date.Date,
                Sum = dto.AccountingPosting.Sum,
                DebitSubconto = dto.AccountingPosting.DebitSubconto,
                CreditSubconto = Map(dto.AccountingPosting.CreditSubconto),
                CreditCode = (int)dto.AccountingPosting.CreditCode,
                Description = dto.AccountingPosting.Description
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
