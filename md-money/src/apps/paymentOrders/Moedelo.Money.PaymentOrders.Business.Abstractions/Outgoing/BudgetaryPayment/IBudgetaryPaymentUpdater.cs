using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentUpdater
    {
        Task UpdateAsync(PaymentOrderSaveRequest request);
    }
}