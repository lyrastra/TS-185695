using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerProvider
    {
        Task ProvideAsync(long documentBaseId);
    }
}