using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.ProductIncomes.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.ProductIncomes
{
    public interface IProductIncomeSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает приходы без документов для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<ProductIncomeSelfCostDto>> GetOnDateAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto request);
    }
}