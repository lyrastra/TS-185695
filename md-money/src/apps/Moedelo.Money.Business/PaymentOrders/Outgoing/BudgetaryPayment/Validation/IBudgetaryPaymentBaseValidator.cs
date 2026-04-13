using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    internal interface IBudgetaryPaymentBaseValidator
    {
        Task ValidateAsync(BudgetaryPaymentBase paymentBase, BudgetaryPeriod period);
        Task ValidateDocumentDateAsync(BudgetaryPaymentSaveRequest request);
        Task ValidateReasonAsync(BudgetaryPaymentSaveRequest request);
    }
}
