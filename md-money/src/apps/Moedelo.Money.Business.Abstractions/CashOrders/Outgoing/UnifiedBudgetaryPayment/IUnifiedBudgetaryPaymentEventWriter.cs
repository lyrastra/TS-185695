using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentEventWriter
    {
        Task WriteCreatedEventAsync(UnifiedBudgetaryPaymentSaveRequest request);
        Task WriteUpdatedEventAsync(
            UnifiedBudgetaryPaymentSaveRequest request,
            OperationType oldOperationType,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds);
        //Task WriteProvideRequiredEventAsync(UnifiedBudgetaryPaymentResponse response);
        Task WriteDeletedEventAsync(
            UnifiedBudgetaryPaymentResponse response,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds);
    }
}
