using System;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Exceptions
{
    public class CashOrderNotFoundExcepton : Exception
    {
        public CashOrderNotFoundExcepton(long documentBaseId)
        {
            DocumentBaseId = documentBaseId;
        }

        public long DocumentBaseId { get; }
    }
}
