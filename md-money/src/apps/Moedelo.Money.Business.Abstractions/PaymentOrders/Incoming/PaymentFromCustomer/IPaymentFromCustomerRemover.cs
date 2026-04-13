using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerRemover
    {
        Task DeleteAsync(long paymentBaseId, long? newDocumentBaseId = null);
    }
}