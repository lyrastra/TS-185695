using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentReader : IPaymentOrderReader<BudgetaryPaymentResponse>
    {
        /// <summary>
        /// Получение операции для копирования. (Обновление данных в копируемом документе на актуальные)
        /// </summary>
        Task<BudgetaryPaymentResponse> GetCopyByBaseIdAsync(long documentBaseId);
    }
}
