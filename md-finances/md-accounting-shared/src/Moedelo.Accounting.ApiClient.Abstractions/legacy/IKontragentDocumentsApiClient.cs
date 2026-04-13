using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.KontragentDocuments.ReconciliationStatements;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IKontragentDocumentsApiClient
    {
        /// <summary>
        /// Возвращает данные для формирования таблицы акта сверки (отправка ON_AKTSVEROTP по ЭДО).
        /// </summary>
        Task<IReadOnlyList<ReportTableCalculationDataDto>> GetReconciliationStatementDataForEdmAsync(
            int firmId, int userId, ReconciliationStatementRequestDto dto);
    }
}