using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentAccountsReader
    {
        Task<IReadOnlyCollection<BudgetaryAccount>> GetAsync();
    }
}