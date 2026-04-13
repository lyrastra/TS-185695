using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerUnprovider
    {
        Task UnprovideAsync(long documentBaseId);
    }
}
