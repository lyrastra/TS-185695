using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;
using System.Threading.Tasks;
using Moedelo.Money.Providing.Business.Abstractions.Models;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierProvider
    {
        Task ProvideAsync(PaymentToSupplierProvideRequest request);
        Task UpdateReserveAsync(SetReserveRequest request);
    }
}
