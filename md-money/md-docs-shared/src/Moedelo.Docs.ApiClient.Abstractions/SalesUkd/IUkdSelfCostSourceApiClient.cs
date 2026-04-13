using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUkd;

public interface IUkdSelfCostSourceApiClient
{
    /// <summary>
    /// Возвращает исходящие УКД для расчёта себестоимости ФИФО
    /// </summary>
    Task<IReadOnlyCollection<SalesUkdSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);
}