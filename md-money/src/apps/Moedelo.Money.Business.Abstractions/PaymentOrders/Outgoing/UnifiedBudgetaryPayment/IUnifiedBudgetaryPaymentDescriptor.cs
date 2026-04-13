using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentDescriptor
    {
        Task<string> GetDescription(IReadOnlyCollection<UnifiedBudgetarySubPaymentForDescription> subPayments, DateTime paymentDate);

        Task<IReadOnlyCollection<UnifiedBudgetarySubPaymentForDescription>> GetSubPayments(string description);
    }
}
