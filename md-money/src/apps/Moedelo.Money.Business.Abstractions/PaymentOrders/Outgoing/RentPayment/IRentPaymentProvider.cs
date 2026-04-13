using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment
{
    public interface IRentPaymentProvider
    {
        Task ProvideAsync(long documentBaseId);
    }
}