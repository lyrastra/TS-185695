using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerAccessor
    {
        bool IsReadOnly(PaymentFromCustomerResponse payment);
    }
}
