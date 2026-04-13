using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentPayerKppSetter
    {
        Task SetPayerKppAsync(long documentBaseId, string kpp);
    }
}
