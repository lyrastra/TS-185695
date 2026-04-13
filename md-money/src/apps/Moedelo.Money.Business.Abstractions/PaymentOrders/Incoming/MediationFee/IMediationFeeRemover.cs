using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeRemover
    {
        Task DeleteAsync(long paymentBaseId, long? newDocumentBaseId = null);
    }
}