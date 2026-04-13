using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierRemover
    {
        Task DeleteAsync(long paymentBaseId, long? newDocumentBaseId = null);
    }
}