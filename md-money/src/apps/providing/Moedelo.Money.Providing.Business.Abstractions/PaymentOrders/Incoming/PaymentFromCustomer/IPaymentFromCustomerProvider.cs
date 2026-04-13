using System.Threading.Tasks;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerProvider
    {
        Task ProvideAsync(PaymentFromCustomerProvideRequest request);
        Task UpdateReserveAsync(SetReserveRequest request);
    }
}
