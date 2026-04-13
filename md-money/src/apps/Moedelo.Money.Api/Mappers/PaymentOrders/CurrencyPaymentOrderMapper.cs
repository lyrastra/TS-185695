using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments.Models;
using Moedelo.Money.Domain.PaymentOrders.Private;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.Api.Mappers.PaymentOrders
{
    static class CurrencyPaymentOrderMapper
    {
        public static PeriodRequest Map(PeriodRequestDto dto)
        {
            return new PeriodRequest
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
        }

        public static CurrencyPaymentOrderDto MapToDto(CurrencyPaymentOrder paymentOrder)
        {
            return new CurrencyPaymentOrderDto
            {
                DocumentBaseId = paymentOrder.DocumentBaseId,
                Date = paymentOrder.Date,
                Number = paymentOrder.Number,
                SettlementAccountId = paymentOrder.SettlementAccountId,
                Sum = paymentOrder.Sum,
                TotalSum = paymentOrder.TotalSum,
                Direction = paymentOrder.Direction,
                OperationType = paymentOrder.OperationType,
                IsPaid = paymentOrder.PaidStatus == PaymentStatus.Payed,
                ProvideInAccounting = paymentOrder.ProvideInAccounting,
                IsBadOperationState = paymentOrder.OperationState.IsBadOperationState()
            };
        }

        public static CurrencyBalanceDto MapToDto(CurrencyBalance balance)
        {
            return new CurrencyBalanceDto
            {
                SettlementAccountId = balance.SettlementAccountId,
                IncomingSum = balance.IncomingSum,
                OutgoingSum = balance.OutgoingSum
            };
        }
    }
}