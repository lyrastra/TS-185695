using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment
{
    public interface IRentPaymentCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(RentPaymentSaveRequest saveRequest);
    }
}
