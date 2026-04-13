using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentReader
    {
        Task<CashOrderResponse> GetByBaseIdAsync(long documentBaseId);
    }
}