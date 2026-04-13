using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.PurchasesCommissionAgentReports.SalesBook;

namespace Moedelo.Docs.Client.PurchasesCommissionAgentReports.SalesBook
{
    /// <summary>
    /// Клиент для получения данных по отчётам посредника (комиссионера) для книги продаж
    /// </summary>
    public interface ICommissionAgentReportSalesBookApiClient
    {
        /// <summary>
        /// Возвращает список отчетов комиссионера сгруппированных по дате отгрузки и типу НДС
        /// </summary>
        Task<IReadOnlyCollection<SalesBookSummaryResponseDto>> GetSummaryAsync(
            int firmId,
            int userId,
            SalesBookSummaryRequestDto requestDto,
            CancellationToken cancellationToken);
    }
}
