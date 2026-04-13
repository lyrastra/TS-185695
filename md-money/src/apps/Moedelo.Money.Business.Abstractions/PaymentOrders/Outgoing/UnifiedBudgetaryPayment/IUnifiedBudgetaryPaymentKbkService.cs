using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentKbkService
    {
        Task<BudgetaryKbkResponse[]> KbkAutocompleteAsync(BudgetaryKbkRequest request);
    }
}
