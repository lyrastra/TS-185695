using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentRemover
    {
        Task DeleteAsync(long documentBaseId);
    }
}