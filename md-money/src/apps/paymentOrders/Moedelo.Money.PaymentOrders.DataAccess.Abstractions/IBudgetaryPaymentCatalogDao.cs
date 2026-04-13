using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IBudgetaryPaymentCatalogDao
    {
        Task<BudgetaryPaymentReason[]> GetPaymentReasonsAsync();
    }
}
