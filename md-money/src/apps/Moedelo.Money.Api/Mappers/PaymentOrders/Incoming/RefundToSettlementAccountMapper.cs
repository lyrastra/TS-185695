using Moedelo.Money.Domain.AccPostings;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models;
using Moedelo.TaxPostings.Enums;
using Moedelo.Money.Api.Models.AccPosting;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class RefundToSettlementAccountMapper
    {
        public static RefundToSettlementAccountResponseDto Map(RefundToSettlementAccountResponse response)
        {
            return new RefundToSettlementAccountResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Contractor),
                ContractorType = response.Contractor?.Type ?? Money.Enums.ContractorType.Ip,
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                //Bills = LinkedDocumentsMapper.MapBillsResponse(response.Bills),
                IsReadOnly = response.IsReadOnly,
                //Nds = MapNds(response),
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                IsFromImport = response.IsFromImport
            };
        }

        public static RefundToSettlementAccountSaveRequest Map(RefundToSettlementAccountSaveDto dto)
        {
            return new RefundToSettlementAccountSaveRequest
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
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                AccPosting = dto.ProvideInAccounting ?? false
                    ? MapCustomAccPosting(dto.Date, dto.AccountingPosting)
                    : null,
                // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
                //BillLinks = LinkedDocumentsMapper.MapBillLinks(dto.Bills),
                //IncludeNds = dto.Nds?.IncludeNds ?? false,
                //NdsType = dto.Nds?.IncludeNds ?? false
                //    ? dto.Nds?.Type
                //    : null,
                //NdsSum = dto.Nds?.IncludeNds ?? false
                //    ? dto.Nds?.Sum
                //    : null,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId
            };
        }

        public static RefundToSettlementAccountSaveRequest ToSaveRequest(this ConfirmRefundToSettlementAccountDto dto)
        {
            return new RefundToSettlementAccountSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(dto.Contractor),
                ContractBaseId = null,
                Description = dto.Description,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                AccPosting = null,
                TaxationSystemType = null,
                PatentId = null,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null
            };
        }

        private static RefundToSettlementAccountCustomAccPosting MapCustomAccPosting(DateTime date, IncomingCustomAccPostingDto dto)
        {
            return new RefundToSettlementAccountCustomAccPosting
            {
                Date = date.Date,
                Sum = dto.Sum,
                DebitSubconto = dto.DebitSubconto,
                CreditCode = (int)dto.CreditCode,
                CreditSubconto = dto.CreditSubconto == null ? new List<Subconto>() : Map(dto.CreditSubconto).ToList(),
                Description = dto.Description
            };
        }

        private static Subconto[] Map(IReadOnlyCollection<SubcontoDto> creditSubconto)
        {
            return creditSubconto.Select(x => new Subconto { Id = x.Id, Name = x.Name }).ToArray();
        }

        //private static NdsResponseDto MapNds(RefundToSettlementAccountResponse operation)
        //{
        //    return new NdsResponseDto
        //    {
        //        IncludeNds = operation.IncludeNds,
        //        Type = operation.IncludeNds
        //            ? operation.NdsType
        //            : null,
        //        Sum = operation.IncludeNds
        //            ? operation.NdsSum
        //            : null
        //    };
        //}
    }
}
