using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentResaveService
    {
        Task ResaveAsync(long documentBaseId);
    }
}
