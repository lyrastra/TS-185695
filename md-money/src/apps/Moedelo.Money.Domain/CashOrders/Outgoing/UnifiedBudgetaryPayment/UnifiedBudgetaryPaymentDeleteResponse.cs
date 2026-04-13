using System.Collections.Generic;

namespace Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentDeleteResponse
    {
        public IReadOnlyCollection<long> DeletedSubPaymentDocumentIds { get; set; }
    }
}
