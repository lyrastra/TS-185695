using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using Moedelo.Money.Domain.SelfCostPayments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers
{
    static class SelfCostPaymentsMapper
    {
        public static SelfCostPaymentDto MapToDto(SelfCostPayment payment)
        {
            return new SelfCostPaymentDto
            {
                DocumentBaseId = payment.DocumentBaseId,
                Date = payment.Date,
                Number = payment.Number,
                Sum = payment.Sum,
                Type = payment.Type,
                RubSum = payment.Type == OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier
                    ? payment.RubSum
                    : null
            };
        }

        public static SelfCostPaymentRequest MapToDomain(SelfCostPaymentRequestDto request)
        {
            return new SelfCostPaymentRequest
            {
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Limit = request.Limit,
                Offset = request.Offset
            };
        }
    }
}
