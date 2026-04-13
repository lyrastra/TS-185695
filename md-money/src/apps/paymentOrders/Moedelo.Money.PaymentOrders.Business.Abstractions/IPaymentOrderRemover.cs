using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrderRemover
    {
        Task DeleteAsync(long documentBaseId);

        Task<long[]> DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
