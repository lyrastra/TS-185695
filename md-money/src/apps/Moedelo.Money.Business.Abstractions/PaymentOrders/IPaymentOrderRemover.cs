using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);

        Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds);

        Task DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
