using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentUpdater
    {
        Task<IReadOnlyCollection<long>> UpdateAsync(PaymentOrderSaveRequest request);
    }
}

