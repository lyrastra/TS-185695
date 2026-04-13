using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class OutgoingCurrencySaleMapper
    {
        public static OutgoingCurrencySaleSaveRequest Map(OutgoingCurrencySaleSaveDto dto)
        {
            return new OutgoingCurrencySaleSaveRequest
            {
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                TotalSum = dto.TotalSum,
                ExchangeRate = dto.ExchangeRate,
                ExchangeRateDiff = dto.ExchangeRateDiff,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static OutgoingCurrencySaleDto Map(OutgoingCurrencySaleResponse response)
        {
            return new OutgoingCurrencySaleDto
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                TotalSum = response.TotalSum,
                ExchangeRate = response.ExchangeRate,
                ExchangeRateDiff = response.ExchangeRateDiff,
                ProvideInAccounting = response.ProvideInAccounting,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsFromImport = response.IsFromImport,
                IsReadOnly = response.IsReadOnly
            };
        }

        public static OutgoingCurrencySaleSaveRequest ToSaveRequest(this ConfirmOutgoingCurrencySaleDto dto)
        {
            return new OutgoingCurrencySaleSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                ExchangeRate = dto.ExchangeRate,
                ToSettlementAccountId = dto.ToSettlementAccountId ?? 0,
                OperationState = dto.ExchangeRate > 0
                    ? OperationState.OutsourceApproved
                    : OperationState.MissingExchangeRate,
                OutsourceState = null,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),

                // нужно рассчитать в бизнес-слое:
                TotalSum = 0,
                ExchangeRateDiff = 0,
            };
        }
    }
}