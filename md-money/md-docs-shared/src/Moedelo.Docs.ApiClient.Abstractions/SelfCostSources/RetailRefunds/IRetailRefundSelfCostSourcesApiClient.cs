using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailRefunds.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.RetailRefunds
{
    public interface IRetailRefundSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает розничный возврат от покупателя для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<RetailRefundSelfCostDto>> GetOnDateAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto request);
    }
}