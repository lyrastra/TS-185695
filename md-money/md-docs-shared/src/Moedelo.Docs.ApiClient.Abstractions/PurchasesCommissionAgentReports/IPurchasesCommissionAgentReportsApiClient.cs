using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports
{
    /// <summary>
    /// Получение данных по отчётам посредника
    /// </summary>
    public interface IPurchasesCommissionAgentReportsApiClient
    {
        /// <summary>
        /// Отчёт посредника (полная мдель) по его базовому идентификатору документа
        /// </summary>
        /// <param name="baseId">Идентификатор базового документа</param>
        /// <returns>Полная модель: документ, позиции, связанные док-ты, возвраты, кто и когда сохранял, можно ли удалить и пр.
        /// </returns>
        Task<CommissionAgentReportResponseDto> GetByDocumentBaseIdAsync(long baseId);

        /// <summary>
        /// Создает отчет
        /// </summary>
        /// <param name="saveRequest">Модель отчета</param>
        /// <param name="querySetting">Настройки запроса (например, таймаут)</param>
        /// <returns>Идентификатор отчета</returns>
        Task<long> Save(CommissionAgentReportSaveRequestDto saveRequest, HttpQuerySetting querySetting = null);

        /// <summary>
        /// Обновляет отчет
        /// <param name="baseId">Идентификатор отчета</param>
        /// <param name="dto">Модель отчета</param>
        /// </summary>
        Task UpdateAsync(long baseId, CommissionAgentReportSaveRequestDto dto);

        /// <summary>
        /// Отчёты посредника (усеченная модель) по списку базовых идентификаторов
        /// </summary>
        /// <returns>Усеченная модель: документ, позиции</returns>
        Task<IReadOnlyCollection<CommissionAgentReportWithItemsPrivateDto>> GetPrivateByDocumentBaseIdsAsync(CommissionAgentReportByBaseIdsRequestDto requestDto);

        /// <summary>
        /// Отчёты посредника (упрощенная модель) по критериям
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentReportPrivateDto>> GetByCriteriaAsync(CommissionAgentReportRequestDto requestDto);

        /// <summary>
        /// Поиск отчетов для возвратов при автоматическом создании. Глубина поиска 1 год от даты документа. Результат отсортирован по убыванию Даты отчета
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentReportForRefundsResponseDto>> GetForRefundsAsync(CommissionAgentReportForRefundsRequestDto requestDto);
    }
}
