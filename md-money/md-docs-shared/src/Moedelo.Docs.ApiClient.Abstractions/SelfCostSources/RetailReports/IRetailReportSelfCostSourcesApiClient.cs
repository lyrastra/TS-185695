using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailReports.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailReports
{
    public interface IRetailReportSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает ОРП для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<RetailReportSelfCostDto>> GetOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);
    }
}