using System;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions
{
    public class PaymentOrderNotFoundExcepton : Exception
    {
        public PaymentOrderNotFoundExcepton(long documentBaseId)
        {
            DocumentBaseId = documentBaseId;
        }

        public long DocumentBaseId { get; }
    }
}
