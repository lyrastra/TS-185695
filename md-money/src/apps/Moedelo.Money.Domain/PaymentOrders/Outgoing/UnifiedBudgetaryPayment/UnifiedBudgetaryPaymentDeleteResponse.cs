using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentDeleteResponse
    {
        public IReadOnlyCollection<long> DeletedSubPaymentDocumentIds { get; set; }
    }
}
