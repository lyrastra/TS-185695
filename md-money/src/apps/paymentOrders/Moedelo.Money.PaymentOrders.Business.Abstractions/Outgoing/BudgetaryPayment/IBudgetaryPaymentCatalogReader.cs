using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentCatalogReader
    {
        Task<BudgetaryPaymentReason[]> GetPaymentReasonsAsync();
    }
}
