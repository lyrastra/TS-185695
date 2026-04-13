using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Statements.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Statements
{
    /// <summary>
    /// Массовое (пере)связывание докумнтов: акты
    /// </summary>
    public interface IStatementRelinkSourceApiClient
    {
        /// <summary>
        /// Из Покупок
        /// </summary>
        Task<IReadOnlyCollection<PurchaseStatementRelinkDto>> GetPurchasesAsync(RelinkSourceRequestDto request);

        /// <summary>
        /// Из Продаж
        /// </summary>
        Task<IReadOnlyCollection<SaleStatementRelinkDto>> GetSalesAsync(RelinkSourceRequestDto request);
    }
}