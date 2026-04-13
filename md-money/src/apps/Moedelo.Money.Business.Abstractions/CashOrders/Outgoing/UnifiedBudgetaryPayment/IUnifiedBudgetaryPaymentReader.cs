using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentReader
    {
        Task<UnifiedBudgetaryPaymentResponse> GetByBaseIdAsync(long documentBaseId);
    }
}
