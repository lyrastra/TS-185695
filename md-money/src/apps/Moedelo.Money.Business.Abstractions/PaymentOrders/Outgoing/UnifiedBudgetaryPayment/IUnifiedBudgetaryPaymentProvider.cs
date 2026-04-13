using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentProvider
    {
        Task ProvideAsync(long documentBaseId);
    }
}
