using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Client.Money
{
    /// <summary>
    /// Нераспознанные платежи ("Красная таблица")
    /// </summary>
    public interface IUnrecognizedPaymentsClient
    {
        /// <summary>
        /// Количество платежей по критериям
        /// </summary>
        Task<int> GetCountAsync(int firmId, int userId, MoneySourceType? sourceType = null, long? sourceId = null);
    }
}