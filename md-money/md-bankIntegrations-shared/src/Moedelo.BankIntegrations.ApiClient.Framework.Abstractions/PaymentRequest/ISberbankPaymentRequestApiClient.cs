using System.Threading.Tasks;
using Moedelo.BankIntegrations.Enums.Acceptance;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.PaymentRequest
{
    /// <summary>
    /// Клиент для работы с ЗДА
    /// </summary>
    public interface ISberbankPaymentRequestApiClient
    {
        /// <summary>
        /// Проверить, есть ли у фирмы ли активный ЗДА по типу
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="type">тип ЗДА</param>
        /// <returns>true/false</returns>
        Task<bool> HasActiveAcceptanceByTypeAsync(int firmId, AcceptanceType type);
        /// <summary>
        /// Разблокировка ЗДА, они блокируются если отзывается согласие или инвалидируется токен, либо, если по р/с в ЗДА нет доступа и получили 403 от банка
        /// </summary>
        Task UnblockSberbankAcceptanceAsync(int firmId);
    }
}
