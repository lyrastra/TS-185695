using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class CurrencyTransferToAccountMapper
    {
        public static CurrencyTransferToAccountResponseDto Map(CurrencyTransferToAccountResponse response)
        {
            return new CurrencyTransferToAccountResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId,
                Description = response.Description,
                IsReadOnly = response.IsReadOnly,
                ProvideInAccounting = response.ProvideInAccounting,
                IsFromImport = response.IsFromImport
            };
        }
        
        public static CurrencyTransferToAccountSaveRequest ToSaveRequest(this ConfirmCurrencyTransferToAccountDto dto)
        {
            return new CurrencyTransferToAccountSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
