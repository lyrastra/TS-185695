using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Purchases;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Sales;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds
{
    public interface IUpdSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает входящие УПД для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<PurchaseUpdSelfCostDto>> GetPurchasesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);

        /// <summary>
        /// Возвращает исходящие УПД для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<SaleUpdSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);
    }
}