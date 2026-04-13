using Moedelo.Money.Api.Models.CashOrders;
using Moedelo.Money.Domain.CashOrders;

namespace Moedelo.Money.Api.Mappers.CashOrders
{
    public static class CashOrderMapper
    {
        public static CashOrderSaveResponseDto MapToResponse(CashOrderSaveResponse model)
        {
            return new CashOrderSaveResponseDto
            {
                DocumentBaseId = model.DocumentBaseId
            };
        }
    }
}