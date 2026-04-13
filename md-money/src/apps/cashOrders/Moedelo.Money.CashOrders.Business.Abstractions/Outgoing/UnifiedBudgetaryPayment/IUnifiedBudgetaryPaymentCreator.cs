using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentCreator
    {
        Task<CreateResponse> CreateAsync(CashOrderSaveRequest request);
    }
}