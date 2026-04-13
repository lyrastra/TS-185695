using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentPayerKppSetter
    {
        Task SetPayerKppAsync(long documentBaseId, string kpp);
    }
}