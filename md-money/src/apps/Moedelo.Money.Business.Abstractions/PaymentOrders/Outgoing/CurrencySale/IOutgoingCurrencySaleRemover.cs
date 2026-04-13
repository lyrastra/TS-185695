using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleRemover
    {
        Task DeleteAsync(long oldDocumentBaseId, long? newDocumentBaseId = null);
    }
}