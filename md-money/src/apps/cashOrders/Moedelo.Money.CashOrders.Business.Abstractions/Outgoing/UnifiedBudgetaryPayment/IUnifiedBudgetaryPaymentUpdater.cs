using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentUpdater
    {
        /// <returns>Список идентификаторов удаленных платежей</returns>
        Task<IReadOnlyCollection<long>> UpdateAsync(CashOrderSaveRequest request);
    }
}