using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentEventWriter
    {
        Task WriteCreatedEventAsync(UnifiedBudgetaryPaymentSaveRequest request);
        Task WriteUpdatedEventAsync(
            UnifiedBudgetaryPaymentSaveRequest request,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds);

        Task WriteProvideRequiredEventAsync(UnifiedBudgetaryPaymentResponse response);
        Task WriteDeletedEventAsync(UnifiedBudgetaryPaymentResponse response, IReadOnlyCollection<long> deletedSubPaymentDocumentIds, long? newDocumentBaseId);
        Task WriteUpdateAfterAccountingStatementCreatedEventAsync(UnifiedBudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest request);
    }
}
