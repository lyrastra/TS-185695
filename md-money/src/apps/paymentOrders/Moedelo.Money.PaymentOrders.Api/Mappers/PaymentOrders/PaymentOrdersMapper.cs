using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders
{
    static class PaymentOrdersMapper
    {
        public static ActualizeRequestItem[] Map(IReadOnlyCollection<ActualizeRequestItemDto> items)
        {
            return items.Select(Map).ToArray();
        }

        public static ActualizeRequestItem Map(ActualizeRequestItemDto dto)
        {
            return new ActualizeRequestItem
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date
            };
        }
    }
}
