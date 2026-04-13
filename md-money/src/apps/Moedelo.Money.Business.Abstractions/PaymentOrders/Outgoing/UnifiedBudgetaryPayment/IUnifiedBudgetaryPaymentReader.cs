using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentReader : IPaymentOrderReader<UnifiedBudgetaryPaymentResponse>
    {
        /// <summary>
        /// Получение операции для копирования. (Обновление данных в копируемом документе на актуальные)
        /// </summary>
        Task<UnifiedBudgetaryPaymentResponse> GetCopyByBaseIdAsync(long documentBaseId);
    }
}
