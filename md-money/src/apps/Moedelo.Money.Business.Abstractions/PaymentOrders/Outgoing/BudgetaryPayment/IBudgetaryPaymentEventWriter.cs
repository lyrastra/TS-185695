using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentEventWriter
    {
        Task WriteCreatedEventAsync(BudgetaryPaymentSaveRequest request);
        Task WriteUpdatedEventAsync(BudgetaryPaymentSaveRequest request);
        Task WriteProvideRequiredEventAsync(BudgetaryPaymentResponse response);
        Task WriteDeletedEventAsync(BudgetaryPaymentResponse response, long? newDocumentBaseId);
        Task WriteUpdateAfterAccountingStatementCreatedEventAsync(BudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest request);
    }
}
