using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment
{
    public interface IRentPaymentRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
        Task DeleteByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
