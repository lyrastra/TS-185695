using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class IncomingCurrencyPurchaseMapper
    {
        public static IncomingCurrencyPurchaseSaveRequest Map(IncomingCurrencyPurchaseSaveDto dto)
        {
            return new IncomingCurrencyPurchaseSaveRequest
            {
                Sum = dto.Sum,
                Date = dto.Date,
                Number = dto.Number,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
            };
        }
        
        public static IncomingCurrencyPurchaseDto Map(IncomingCurrencyPurchaseResponse response)
        {
            return new IncomingCurrencyPurchaseDto
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                SettlementAccountId = response.SettlementAccountId,
                FromSettlementAccountId = response.FromSettlementAccountId.GetValueOrDefault(),
                ProvideInAccounting = response.ProvideInAccounting,
                IsFromImport = response.IsFromImport,
                IsReadOnly = response.IsReadOnly
            };
        }

        internal static IncomingCurrencyPurchaseSaveRequest ToSaveRequest(this ConfirmIncomingCurrencyPurchaseDto dto)
        {
            return dto is null
                ? null
                : new IncomingCurrencyPurchaseSaveRequest
                {
                    DocumentBaseId = dto.DocumentBaseId,
                    Date = dto.Date,
                    Number = dto.Number,
                    Description = dto.Description,
                    SettlementAccountId = dto.SettlementAccountId,
                    Sum = dto.Sum,
                    ProvideInAccounting = true,
                    OperationState = OperationState.OutsourceApproved,
                    OutsourceState = null,
                    FromSettlementAccountId = dto.FromSettlementAccountId,
                };
        }
    }
}