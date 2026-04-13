using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonUpdater : IPaymentOrderUpdater<PaymentToAccountablePersonSaveRequest, PaymentOrderSaveResponse>
    {
    }
}