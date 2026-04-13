using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeRemover
    {
        Task DeleteAsync(long paymentBaseId, long? newDocumentBaseId = null);
    }
}