using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentTaxPostingValidator
    {
        Task ValidateAsync(BudgetaryPaymentSaveRequest request);
    }
}
