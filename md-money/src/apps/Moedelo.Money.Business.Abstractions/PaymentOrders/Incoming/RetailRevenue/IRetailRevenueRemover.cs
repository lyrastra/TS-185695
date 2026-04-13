using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueRemover
    {
        Task DeleteAsync(long paymentBaseId, long? newDocumentBaseId = null);
    }
}