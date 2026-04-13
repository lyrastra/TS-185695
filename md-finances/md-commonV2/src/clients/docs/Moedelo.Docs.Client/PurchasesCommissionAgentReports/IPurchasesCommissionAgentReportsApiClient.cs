using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports;
using Moedelo.Docs.Dto.ProductMerge;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.PurchasesCommissionAgentReports
{
    /// <summary>
    /// клиент для получения данных по отчётам посредника (комиссионера)
    /// </summary>
    public interface IPurchasesCommissionAgentReportsApiClient : IDI
    {
        /// <summary>
        /// Возвращает список отчетов комиссионера
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentReportShortDto>> GetByCriteriaAsync(int firmId,
            int userId,
            CommissionAgentReportRequestDto requestDto, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список отчетов с позициями по списку BaseId
        /// </summary>
        /// <param name="firmId">фирма</param>
        /// <param name="userId">пользователь</param>
        /// <param name="requestDto">параметры запроса</param>
        Task<IReadOnlyCollection<CommissionAgentReportWithItemsDto>> GetWithItemsByBaseIdsAsync(
            int firmId, 
            int userId,
            CommissionAgentReportByBaseIdsRequestDto requestDto);

        /// <summary>
        /// Объединение номенклатур в позициях отчетов комиссионеров
        /// </summary>
        /// <param name="firmId">фирма</param>
        /// <param name="userId">пользователь</param>
        /// <param name="requestDto">параметры запроса</param>
        /// <returns>Возвращает список BaseId, в которых произведены замены</returns>
        Task<IReadOnlyCollection<long>> MergeProductsAsync(
            int firmId, 
            int userId,
            ProductMergeRequestDto requestDto);

        /// <summary>
        /// Возвращает отчёт комиссионера как документ основание для сф и УПД с типом 1
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentReportAsReasonDocumentDto>> GetAsReasonDocumentsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Возвращает отчёты комиссионера для автокомплита документа основания в счет-фактурах
        /// </summary>
        Task<IReadOnlyCollection<ReasonDocumentsAutocompleteResponseDto>> GetReasonDocumentsAutocompleteAsync(
            int firmId,
            int userId,
            ReasonDocumentsAutocompleteRequestDto request);
    }
}