using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class OutgoingCurrencyPurchaseMapper
    {
        public static OutgoingCurrencyPurchaseSaveRequest Map(OutgoingCurrencyPurchaseSaveDto dto)
        {
            return new OutgoingCurrencyPurchaseSaveRequest
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

        public static OutgoingCurrencyPurchaseDto Map(OutgoingCurrencyPurchaseResponse response)
        {
            return new OutgoingCurrencyPurchaseDto
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
                IsReadOnly = response.IsReadOnly,
            };
        }

        public static OutgoingCurrencyPurchaseSaveRequest ToSaveRequest(this ConfirmOutgoingCurrencyPurchaseDto dto)
        {
            return new OutgoingCurrencyPurchaseSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                ExchangeRate = dto.ExchangeRate,
                ProvideInAccounting = true,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                // рассчитывается в бизнес-слое
                TotalSum = 0,
                ExchangeRateDiff = 0,
            };
        }
    }
}