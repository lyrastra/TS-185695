using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsUpdater : IPaymentOrderUpdater<PaymentToNaturalPersonsSaveRequest, PaymentOrderSaveResponse>
    {
    }
}