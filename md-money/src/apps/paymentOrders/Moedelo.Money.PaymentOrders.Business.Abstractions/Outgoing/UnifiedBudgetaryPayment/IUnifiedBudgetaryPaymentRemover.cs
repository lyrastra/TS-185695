using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentRemover
    {
        Task<IReadOnlyList<long>> DeleteAsync(long documentBaseId);
    }
}