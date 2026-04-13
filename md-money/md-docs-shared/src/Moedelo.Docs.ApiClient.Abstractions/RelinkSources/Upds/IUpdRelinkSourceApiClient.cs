using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Upds.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Upds
{
    /// <summary>
    /// Массовое (пере)связывание докумнтов: УПД
    /// </summary>
    public interface IUpdRelinkSourceApiClient
    {
        /// <summary>
        /// Из Покупок
        /// </summary>
        Task<IReadOnlyCollection<PurchaseUpdRelinkDto>> GetPurchasesAsync(RelinkSourceRequestDto request);

        /// <summary>
        /// Из Продаж
        /// </summary>
        Task<IReadOnlyCollection<SaleUpdRelinkDto>> GetSalesAsync(RelinkSourceRequestDto request);
    }
}