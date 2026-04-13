using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CommissionAgentReports.Models;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CommissionAgentReports
{
    public interface ICommissionAgentReportSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает отчет комиссионера для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentReportSelfCostDto>> GetOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);
    }
}