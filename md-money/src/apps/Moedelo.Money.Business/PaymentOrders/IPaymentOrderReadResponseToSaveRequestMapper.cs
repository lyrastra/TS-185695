using System;

namespace Moedelo.Money.Business.PaymentOrders
{
    public interface IPaymentOrderReadResponseToSaveRequestMapper<TReadResponse, TSaveRequest>
    {
        Func<TReadResponse, TSaveRequest> Mapper { get; }
    }
}
