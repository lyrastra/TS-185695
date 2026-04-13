using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentSaveResponse
    {
        public IReadOnlyCollection<long> DeletedSubPaymentDocumentIds { get; set; }
    }
}
