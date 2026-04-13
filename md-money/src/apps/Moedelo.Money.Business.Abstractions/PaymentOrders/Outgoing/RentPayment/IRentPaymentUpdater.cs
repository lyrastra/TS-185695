using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment
{
    public interface IRentPaymentUpdater : IPaymentOrderUpdater<RentPaymentSaveRequest, PaymentOrderSaveResponse>
    {

    }
}
