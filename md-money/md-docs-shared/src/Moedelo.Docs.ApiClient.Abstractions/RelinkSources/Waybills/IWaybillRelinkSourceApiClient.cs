using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Waybills.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.RelinkSources.Waybills
{
    /// <summary>
    /// Массовое (пере)связывание докумнтов: накладные
    /// </summary>
    public interface IWaybillRelinkSourceApiClient
    {
        /// <summary>
        /// Из Покупок
        /// </summary>
        Task<IReadOnlyCollection<PurchaseWaybillRelinkDto>> GetPurchasesAsync(RelinkSourceRequestDto request);

        /// <summary>
        /// Из Продаж
        /// </summary>
        Task<IReadOnlyCollection<SaleWaybillRelinkDto>> GetSalesAsync(RelinkSourceRequestDto request);
    }
}