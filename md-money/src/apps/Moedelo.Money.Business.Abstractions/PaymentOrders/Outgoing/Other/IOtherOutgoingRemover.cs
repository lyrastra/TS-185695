using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other
{
    public interface IOtherOutgoingRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
    }
}