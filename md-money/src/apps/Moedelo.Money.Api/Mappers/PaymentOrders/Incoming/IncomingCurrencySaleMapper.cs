using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class IncomingCurrencySaleMapper
    {
        public static IncomingCurrencySaleSaveRequest Map(IncomingCurrencySaleSaveDto dto)
        {
            return new IncomingCurrencySaleSaveRequest
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
        
        public static IncomingCurrencySaleDto Map(IncomingCurrencySaleResponse response)
        {
            return new IncomingCurrencySaleDto
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                SettlementAccountId = response.SettlementAccountId,
                FromSettlementAccountId = response.FromSettlementAccountId,
                ProvideInAccounting = response.ProvideInAccounting,
                IsFromImport = response.IsFromImport,
                IsReadOnly = response.IsReadOnly,
            };
        }

        internal static IncomingCurrencySaleSaveRequest ToSaveRequest(this ConfirmIncomingCurrencySaleDto dto)
        {
            return dto is null
                ? null
                : new IncomingCurrencySaleSaveRequest
                {
                    DocumentBaseId = dto.DocumentBaseId,
                    Date = dto.Date,
                    Number = dto.Number,
                    Description = dto.Description,
                    SettlementAccountId = dto.SettlementAccountId,
                    FromSettlementAccountId = dto.FromSettlementAccountId,
                    Sum = dto.Sum,
                    ProvideInAccounting = true,
                    OperationState = dto.FromSettlementAccountId > 0
                        ? OperationState.OutsourceApproved
                        : OperationState.MissingCurrencySettlementAccount,
                    OutsourceState = null,
                };
        }
    }
}