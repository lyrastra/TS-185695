using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerReader : IPaymentOrderReader<RefundToCustomerResponse>
    {
    }
}
