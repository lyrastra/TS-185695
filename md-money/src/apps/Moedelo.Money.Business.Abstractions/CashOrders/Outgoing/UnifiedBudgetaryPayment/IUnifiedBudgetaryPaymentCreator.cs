using Moedelo.Money.Domain.CashOrders;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentCreator
    {
        Task<CashOrderSaveResponse> CreateAsync(UnifiedBudgetaryPaymentSaveRequest request);
    }
}