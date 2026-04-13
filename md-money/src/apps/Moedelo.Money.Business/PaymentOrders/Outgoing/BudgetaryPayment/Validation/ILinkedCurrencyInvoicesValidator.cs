using System.Threading.Tasks;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    public interface ILinkedCurrencyInvoicesValidator
    {
        Task ValidateAsync(BudgetaryPaymentSaveRequest request, Kbk kbk);
    }
}