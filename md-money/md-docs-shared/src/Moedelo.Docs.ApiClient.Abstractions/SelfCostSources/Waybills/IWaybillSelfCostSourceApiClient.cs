using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Purchases;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills.Models.Sales;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Waybills
{
    public interface IWaybillSelfCostSourceApiClient
    {
        /// <summary>
        /// Возвращает входящие накладные для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<PurchaseWaybillSelfCostDto>> GetPurchasesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);

        /// <summary>
        /// Возвращает исходящие накладные для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<SaleWaybillSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);
    }
}