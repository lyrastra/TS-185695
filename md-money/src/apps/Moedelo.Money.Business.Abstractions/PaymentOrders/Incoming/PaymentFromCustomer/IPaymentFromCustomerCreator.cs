using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(PaymentFromCustomerSaveRequest saveRequest);
    }
}