using Moedelo.Money.Api.Models.CashOrders.Outgoing;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Api.Mappers.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public static class UnifiedBudgetaryPaymentMapper
    {
        public static UnifiedBudgetaryPaymentResponseDto Map(UnifiedBudgetaryPaymentResponse response)
        {
            return new UnifiedBudgetaryPaymentResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                CashId = response.CashId,
                Recipient = response.Recipient,
                Destination = response.Destination,
                SubPayments = MapSubpaymentsToDto(response.SubPayments),
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly
            };
        }

        public static UnifiedBudgetaryPaymentSaveRequest Map(UnifiedBudgetaryPaymentSaveDto dto)
        {
            return new UnifiedBudgetaryPaymentSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                CashId = dto.CashId,
                Recipient = dto.Recipient,
                Destination = dto.Destination,
                SubPayments = MapSubpaymentsToDomain(dto.SubPayments),
                ProvideInAccounting = dto.ProvideInAccounting ?? true
            };
        }

        private static UnifiedBudgetarySubPaymentResponseDto[] MapSubpaymentsToDto(
            IReadOnlyCollection<UnifiedBudgetarySubPaymentResponseModel> subPayments)
        {
            if (subPayments == null || subPayments.Count == 0)
            {
                return Array.Empty<UnifiedBudgetarySubPaymentResponseDto>();
            }

            return subPayments.Select(MapToDto).ToArray();
        }

        private static UnifiedBudgetarySubPaymentResponseDto MapToDto(UnifiedBudgetarySubPaymentResponseModel subPayment)
        {
            return new UnifiedBudgetarySubPaymentResponseDto
            {
                DocumentBaseId = subPayment.DocumentBaseId,
                Kbk = new UnifiedBudgetaryKbkResponseDto
                {
                    Id = subPayment.Kbk.Id,
                    Number = subPayment.Kbk.Number,
                    AccountCode = subPayment.Kbk.AccountCode,
                },
                Sum = subPayment.Sum,
                PatentId = subPayment.PatentId,
                Period = BudgetaryPeriodMapper.MapToDto(subPayment.Period),
                TaxPostingsInManualMode = subPayment.TaxPostingsInManualMode
            };
        }

        private static UnifiedBudgetarySubPaymentSaveModel[] MapSubpaymentsToDomain(
            IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveDto> subPayments)
        {
            if (subPayments == null || subPayments.Count == 0)
            {
                return Array.Empty<UnifiedBudgetarySubPaymentSaveModel>();
            }

            return subPayments.Select(MapToDomain).ToArray();
        }

        private static UnifiedBudgetarySubPaymentSaveModel MapToDomain(UnifiedBudgetarySubPaymentSaveDto dto)
        {
            return new UnifiedBudgetarySubPaymentSaveModel
            {
                DocumentBaseId = dto.DocumentBaseId ?? 0,
                KbkId = dto.KbkId,
                Sum = dto.Sum,
                PatentId = dto.PatentId,
                Period = BudgetaryPeriodMapper.MapToDomain(dto.Period),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
            };
        }
    }
}