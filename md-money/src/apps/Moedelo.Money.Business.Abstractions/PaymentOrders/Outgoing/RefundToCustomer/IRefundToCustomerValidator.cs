using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerValidator
    {
        Task ValidateAsync(RefundToCustomerSaveRequest request);
    }
}