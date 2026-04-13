using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(RefundToCustomerSaveRequest saveRequest);
    }
}