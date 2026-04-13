using Moedelo.Money.Business.Numeration.PaymentOrder.ApiClient.Dto;
using Moedelo.Money.Domain.PaymentOrderNumeration;

namespace Moedelo.Money.Business.Numeration.PaymentOrder
{
    internal static class PaymentOrderNumerationMapper
    {
        public static PaymentOrderNumerationDto MapToDto(PaymentOrderNumerationData model)
        {
            return new PaymentOrderNumerationDto()
            {
                LastNumber = model.LastNumber,
                SettlementAccountId = model.SettlementAccountId,
                Year = model.Year
            };
        }
    }
}
