using Moedelo.Money.Api.Models.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class AccrualOfInterestMapper
    {
        public static AccrualOfInterestResponseDto Map(AccrualOfInterestResponse response)
        {
            return new AccrualOfInterestResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                TaxationSystemType = response.TaxationSystemType,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static AccrualOfInterestSaveRequest Map(AccrualOfInterestSaveDto dto)
        {
            return new AccrualOfInterestSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description,
                TaxationSystemType = dto.TaxationSystemType,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming)
            };
        }

        public static AccrualOfInterestSaveRequest ToSaveRequest(this ConfirmAccrualOfInterestDto dto)
        {
            return new AccrualOfInterestSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                OutsourceState = null,
                OperationState = OperationState.OutsourceApproved,
            };
        }
    }
}
