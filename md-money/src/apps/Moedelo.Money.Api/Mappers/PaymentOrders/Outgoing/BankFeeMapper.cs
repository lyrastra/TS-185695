using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class BankFeeMapper
    {
        public static BankFeeResponseDto Map(BankFeeResponse response)
        {
            return new BankFeeResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                TaxationSystemType = response.TaxationSystemType,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                BankName = response.BankName, 
                IsReadOnly = response.IsReadOnly,
                PatentId = response.PatentId,
                IsFromImport = response.IsFromImport,
            };
        }

        public static BankFeeSaveRequest Map(BankFeeSaveDto dto)
        {
            return new BankFeeSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                TaxationSystemType = dto.TaxationSystemType,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                PatentId = dto.PatentId,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static BankFeeSaveRequest ToSaveRequest(this ConfirmBankFeeDto dto)
        {
            return new BankFeeSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = true,
                TaxationSystemType = dto.TaxationSystemType,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),
                PatentId = dto.PatentId,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
