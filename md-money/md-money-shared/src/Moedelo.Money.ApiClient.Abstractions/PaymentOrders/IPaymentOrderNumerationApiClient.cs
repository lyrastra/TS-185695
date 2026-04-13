using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders
{
    public interface IPaymentOrderNumerationApiClient
    {
        /// <summary>
        /// Получить список минимальных свободных номеров ПП
        /// </summary>
        /// <param name="settlementAccountId">Идентификатор расчетного счета(обязательно > 0)</param>
        /// <param name="year">Год нумерации(обязательно > 0)</param>
        /// <param name="count">Требуесое колличество номеров(обязательно > 0)</param>
        /// <returns></returns>
        Task<IReadOnlyList<int>> GetNextNumbersAsync(int settlementAccountId, int year, int count);
    }
}