using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements.Models;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements
{
    public interface IAdvanceStatementSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает АО для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<AdvanceStatementSelfCostDto>> GetOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);
    }
}