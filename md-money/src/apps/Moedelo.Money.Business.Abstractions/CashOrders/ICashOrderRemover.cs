using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders
{
    public interface ICashOrderRemover
    {
        Task DeleteAsync(long documentBaseId);
        Task DeleteAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}
