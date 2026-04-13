using Moedelo.Money.Domain.PaymentOrders;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderAccessor
    {
        bool IsReadOnly(IAccessorPropsResponse payment);
    }
}
