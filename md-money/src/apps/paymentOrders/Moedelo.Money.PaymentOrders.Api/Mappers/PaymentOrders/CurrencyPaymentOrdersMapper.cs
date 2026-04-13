using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders
{
    static class CurrencyPaymentOrdersMapper
    {
        public static PeriodRequest Map(PeriodRequestDto dto)
        {
            return new PeriodRequest
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
        }

        public static CurrencyPaymentOrderDto MapToDto(PaymentOrder paymentOrder)
        {
            return new CurrencyPaymentOrderDto
            {
                DocumentBaseId = paymentOrder.DocumentBaseId,
                Date = paymentOrder.Date,
                Number = paymentOrder.Number,
                SettlementAccountId = paymentOrder.SettlementAccountId,
                Sum = paymentOrder.Sum,
                TotalSum = paymentOrder.TotalSum ?? 0,
                Direction = paymentOrder.Direction,
                OperationType = paymentOrder.OperationType,
                ProvideInAccounting = paymentOrder.ProvideInAccounting,
                OperationState = paymentOrder.OperationState,
                PaidStatus = paymentOrder.PaidStatus
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