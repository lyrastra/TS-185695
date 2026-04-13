using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders
{
    internal interface IConcretePaymentOrderCreator<TSaveRequest>
    {
        Task<PaymentOrderSaveResponse> CreateAsync(TSaveRequest request);
    }
}
