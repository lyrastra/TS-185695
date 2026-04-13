using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentRemover
    {
        Task<IReadOnlyCollection<long>> DeleteAsync(long documentBaseId);
    }
}