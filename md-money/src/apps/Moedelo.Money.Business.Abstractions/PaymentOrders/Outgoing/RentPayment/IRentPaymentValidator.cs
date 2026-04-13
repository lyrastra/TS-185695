using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment
{
    public interface IRentPaymentValidator
    {
        Task ValidateAsync(RentPaymentSaveRequest request);
    }
}
