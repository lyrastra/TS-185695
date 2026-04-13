using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders;

namespace Moedelo.Money.Api.Mappers.PaymentOrders
{
    public static class PaymentOrderMapper
    {
        public static PaymentOrderSaveResponseDto MapToResponse(PaymentOrderSaveResponse model)
        {
            return new PaymentOrderSaveResponseDto
            {
                DocumentBaseId = model.DocumentBaseId
            };
        }
    }
}