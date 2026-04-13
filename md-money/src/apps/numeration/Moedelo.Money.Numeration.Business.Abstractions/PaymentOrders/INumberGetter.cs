using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders
{
    public interface INumberGetter
    {
        /// <summary>
        /// Получение последнего записанного номера
        /// </summary>
        Task<int> Last(int settlementAccountId, int year);

        /// <summary>
        /// Получение последнего записанного номера и следующих номеров
        /// </summary>
        Task<(int, IReadOnlyCollection<int>)> LastAndNext(int settlementAccountId, int year, int? count = null);
    }
}