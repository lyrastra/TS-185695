using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class CurrencyBankFeeMapper
    {
        public static CurrencyBankFeeResponseDto Map(CurrencyBankFeeResponse response)
        {
            return new CurrencyBankFeeResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsReadOnly = response.IsReadOnly,
                TotalSum = response.TotalSum,
                IsFromImport = response.IsFromImport
            };
        }

        public static CurrencyBankFeeSaveRequest Map(CurrencyBankFeeSaveDto dto)
        {
            return new CurrencyBankFeeSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings,TaxPostingDirection.Outgoing),
                TotalSum = dto.TotalSum,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static CurrencyBankFeeSaveRequest ToSaveRequest(this ConfirmCurrencyBankFeeDto dto)
        {
            return new CurrencyBankFeeSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}