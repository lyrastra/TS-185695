using System.Collections.Generic;

namespace Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentUpdateResponseDto
    {
        public IReadOnlyCollection<long> DeletedSubPaymentDocumentIds { get; set; }
    }
}
